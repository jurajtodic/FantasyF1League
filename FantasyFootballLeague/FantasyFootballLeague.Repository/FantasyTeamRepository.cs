using FantasyFootballLeague.Common;
using FantasyFootballLeague.Model;
using FantasyFootballLeague.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Repository
{
    public class FantasyTeamRepository : IFantasyTeamRepository
    {
        private string connectionString = Environment.GetEnvironmentVariable("connectionString");
        int index = 0;
        private bool FlagForConnStringAdjMenthod = false;

        public void StringAdjustmentMethod()
        {
            if (connectionString.Length >= 80)
            {
                StringBuilder sb = new StringBuilder(connectionString);
                this.index = connectionString.IndexOf("\\");
                sb.Remove(this.index, 1);
                this.connectionString = sb.ToString();
                FlagForConnStringAdjMenthod = true;
            }
        }

        public async Task<List<FantasyTeam>> GetFantasyTeamDataAsync(FantasyTeamFilter filter, FantasyTeamPaging paging, FantasyTeamSorting sorting)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            List<FantasyTeam> fantasyTeamList = new List<FantasyTeam>();
            SqlConnection connection = new SqlConnection(connectionString);

            using(connection)
            {
                StringBuilder stringBuilder = new StringBuilder("SELECT * FROM FantasyTeam ", 500);
                if (filter != null)
                {
                    stringBuilder.Append("WHERE 1=1 ");
                    if (filter.SearchString != null)
                    {
                        stringBuilder.AppendFormat("AND FantasyTeamName LIKE '%{0}%' ", filter.SearchString);
                    }
                    // FILTER
                    if (filter.LowerTotalPoints != null && filter.HigherTotalPoints != null)
                    {
                        stringBuilder.AppendFormat("AND TotalPoints BETWEEN {0} AND {1} ", filter.LowerTotalPoints, filter.HigherTotalPoints);
                    }
                    else
                    {
                        if (filter.HigherTotalPoints != null)
                        {
                            stringBuilder.AppendFormat("AND TotalPoints > {0} ", filter.HigherTotalPoints);
                        }
                        if (filter.LowerTotalPoints != null)
                        {
                            stringBuilder.AppendFormat("AND TotalPoints < {0} ", filter.LowerTotalPoints);
                        }
                    }
                }

                // SORTING
                stringBuilder.AppendFormat("ORDER BY {0} {1} ", sorting.OrderBy, sorting.SortOrder);
                // PAGING
                stringBuilder.Append("OFFSET @lowerLimit ROWS FETCH NEXT @rpp ROWS ONLY; ");

                SqlCommand command = new SqlCommand(Convert.ToString(stringBuilder), connection);
                command.Parameters.AddWithValue("@lowerLimit", (paging.PageNumber - 1) * paging.Rpp);
                command.Parameters.AddWithValue("@rpp", paging.Rpp);

                command.Connection = connection;
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            fantasyTeamList.Add(new FantasyTeam(
                                reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3),
                                reader.GetDateTime(4), reader.GetDateTime(5), reader.GetGuid(6), reader.GetDouble(7),reader.GetInt32(8))
                                );
                        }
                    }
                    else
                    {
                        string.Format("Sql error {0}, number {1}", 2, 1);
                        return fantasyTeamList;
                    }
                    return fantasyTeamList;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }
        }

        public async Task<FantasyTeam> GetFantasyTeamDataByIdAsync(System.Guid id)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }
            SqlConnection connection = new SqlConnection(connectionString);
            List<FantasyTeam> fantasyTeamList = new List<FantasyTeam>();

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM FantasyTeam WHERE FantasyTeamId = @id; ", connection);
                command.Parameters.AddWithValue("@id", id);
                command.Connection = connection;
                try
                {
                    await connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            fantasyTeamList.Add(new FantasyTeam(
                                reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3),
                                reader.GetDateTime(4), reader.GetDateTime(5), reader.GetGuid(6), reader.GetDouble(7), reader.GetInt32(8))
                                );
                        }
                    }
                    else
                    {
                        string.Format("Sql error {0}, number {1}", 2, 1);
                        return null;
                    }
                    return fantasyTeamList[0];
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }

            }
        }
        
        public async Task<FantasyTeam> PostFantasyTeamDataAsync(FantasyTeam fantasyTeam)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand("INSERT INTO FantasyTeam VALUES (@id, @name, @username, (SELECT MaximumFreeSubs FROM FantasyLeague WHERE FantasyLeagueId = @fantasyLeagueId), @creationDate, @lastUpdated, (SELECT FantasyLeagueId FROM FantasyLeague WHERE FantasyLeagueId = @fantasyLeagueId), (SELECT FantasyLeague.Budget FROM FantasyLeague WHERE FantasyLeagueId = @fantasyLeagueId),@totalPoints) ", connection);
                command.Parameters.AddWithValue("@id", fantasyTeam.FantasyTeamId);
                command.Parameters.AddWithValue("@name", fantasyTeam.FantasyTeamName);
                command.Parameters.AddWithValue("@username", fantasyTeam.Username);
                command.Parameters.AddWithValue("@creationDate", fantasyTeam.CreationDate);
                command.Parameters.AddWithValue("@lastUpdated", fantasyTeam.LastUpdated);
                command.Parameters.AddWithValue("@fantasyLeagueId", fantasyTeam.FantasyLeagueId);
                command.Parameters.AddWithValue("@totalPoints", fantasyTeam.TotalPoints);


                command.Connection = connection;
                FantasyTeam returnFantasyTeam = new FantasyTeam(fantasyTeam.FantasyTeamId, fantasyTeam.FantasyTeamName, fantasyTeam.Username, fantasyTeam.FreeSubsLeft, fantasyTeam.CreationDate, fantasyTeam.LastUpdated, fantasyTeam.FantasyLeagueId, fantasyTeam.RemainingBudget, fantasyTeam.TotalPoints);
                try
                {
                    await connection.OpenAsync();
                    command.ExecuteNonQuery();
                    return returnFantasyTeam;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }
        }
        
        public async Task<FantasyTeam> PutFantasyTeamDataAsync(Guid id, FantasyTeam fantasyTeam)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamId = @fTeamid ", connection);
                command.Parameters.AddWithValue("@fTeamid", id);
                connection.Open();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            reader.Close();
                            cmd.Connection = connection;
                            cmd.CommandText = "UPDATE FantasyTeam SET FantasyTeamName=@fTeamName, Username = @fTeamUsername, FreeSubsLeft=@fTeamSubsLeft, LastUpdated=@lastUpdated, RemainingBudget = @remainingBudget, TotalPoints = @totalPoints WHERE FantasyTeamId = @fTeamId ";
                            cmd.Parameters.AddWithValue("@fTeamName", fantasyTeam.FantasyTeamName);
                            cmd.Parameters.AddWithValue("@fTeamUsername", fantasyTeam.Username);
                            cmd.Parameters.AddWithValue("@fTeamSubsLeft", fantasyTeam.FreeSubsLeft);
                            cmd.Parameters.AddWithValue("@lastUpdated", fantasyTeam.LastUpdated);
                            cmd.Parameters.AddWithValue("@fTeamId", id);
                            cmd.Parameters.AddWithValue("@remainingBudget", fantasyTeam.RemainingBudget);
                            cmd.Parameters.AddWithValue("@totalPoints", fantasyTeam.TotalPoints);



                            FantasyTeam returnFantasyTeam = new FantasyTeam(id, fantasyTeam.FantasyTeamName, fantasyTeam.Username, fantasyTeam.FreeSubsLeft, fantasyTeam.CreationDate, fantasyTeam.LastUpdated, fantasyTeam.FantasyLeagueId, fantasyTeam.RemainingBudget, fantasyTeam.TotalPoints);

                            try
                            {
                                await cmd.ExecuteNonQueryAsync();
                                connection.Close();
                                return returnFantasyTeam;
                            }
                            catch (SqlException e)
                            {
                                throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
                }              
            }
        }

        public async Task<bool> DeleteFantasyTeamDataAsync(Guid id)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamId = @fTeamid ", connection);
                command.Parameters.AddWithValue("@fTeamid", id);
                connection.Open();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            reader.Close();
                            cmd.Connection = connection;

                            cmd.CommandText = "DELETE FROM FantasyTeam WHERE FantasyTeamId = @id ";
                            cmd.Parameters.AddWithValue("@id", id);
                            try
                            {
                                await cmd.ExecuteNonQueryAsync();
                                connection.Close();
                                return true;
                            }
                            catch (SqlException e)
                            {
                                throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public async Task<List<Driver>> GetDriversFromFantasyTeam(Guid fantasyTeamId)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            List<Driver> driverList = new List<Driver>();
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand($"SELECT Driver.DriverId, Driver.DriverName, Driver.DriverSurname, Driver.Age, " +
                    $"Driver.IsTurboDriver, Driver.Price, Driver.TotalPoints, Driver.CreationDate, " +
                    $"Driver.LastUpdated, Driver.ConstructorId, Driver.ScoringRulesId " +
                    $"FROM FantasyTeamToDriver " +
                    $"INNER JOIN Driver ON FantasyTeamToDriver.DriverId = Driver.DriverId " +
                    $"INNER JOIN FantasyTeam ON FantasyTeam.FantasyTeamId = FantasyTeamToDriver.FantasyTeamId " +
                    $"WHERE FantasyTeam.FantasyTeamId = @fantasyTeamId ");
                command.Parameters.AddWithValue("@fantasyTeamId", fantasyTeamId);
                command.Connection = connection;
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            driverList.Add(new Driver(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3),
                                                    reader.GetBoolean(4), reader.GetDouble(5), reader.GetInt32(6), reader.GetDateTime(7),
                                                    reader.GetDateTime(8), reader.GetGuid(9), reader.GetGuid(10)));
                        }
                    }
                    else
                    {
                        string.Format("Sql error {0}, number {1}", 2, 1);
                        return driverList;
                    }
                    return driverList;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }
        }

        public async Task<FantasyTeam> UpdateFantasyTeamAfterAddingNewDriver(Guid fantasyTeamId, double driverPrice, int driverTotalPoints)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand("UPDATE FantasyTeam SET RemainingBudget=RemainingBudget-@driverPrice, TotalPoints=TotalPoints+@driverPoints WHERE FantasyTeamId= @fantasyTeamId ", connection);
                command.Parameters.AddWithValue("@fantasyTeamId", fantasyTeamId);
                command.Parameters.AddWithValue("@driverPrice", driverPrice);
                command.Parameters.AddWithValue("@driverPoints", driverTotalPoints);


                command.Connection = connection;
                FantasyTeam returnFantasyTeam = await GetFantasyTeamDataByIdAsync(fantasyTeamId);
                try
                {
                    await connection.OpenAsync();
                    command.ExecuteNonQuery();
                    return returnFantasyTeam;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }
        }

        public async Task<FantasyTeam> UpdateFantasyTeamAfterDeletingDriver(Guid fantasyTeamId, double driverPrice, int driverTotalPoints)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand("UPDATE FantasyTeam SET FreeSubsLeft = FreeSubsLeft-1, RemainingBudget=RemainingBudget+@refund, TotalPoints=TotalPoints-@driverPoints WHERE FantasyTeamId= @fantasyTeamId ", connection);
                command.Parameters.AddWithValue("@fantasyTeamId", fantasyTeamId);
                command.Parameters.AddWithValue("@refund", driverPrice/2);
                command.Parameters.AddWithValue("@driverPoints", driverTotalPoints);


                command.Connection = connection;
                FantasyTeam returnFantasyTeam = await GetFantasyTeamDataByIdAsync(fantasyTeamId);
                try
                {
                    await connection.OpenAsync();
                    command.ExecuteNonQuery();
                    return returnFantasyTeam;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }
        }

        public async Task<FantasyTeam> UpdateFantasyTeamAfterChangingDriverPoints(Guid fantasyTeamId, double driverPrice, int driverTotalPoints)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand("UPDATE FantasyTeam SET RemainingBudget=RemainingBudget+@refund, TotalPoints=TotalPoints-@driverPoints WHERE FantasyTeamId= @fantasyTeamId ", connection);
                command.Parameters.AddWithValue("@fantasyTeamId", fantasyTeamId);
                command.Parameters.AddWithValue("@refund", driverPrice);
                command.Parameters.AddWithValue("@driverPoints", driverTotalPoints);

                command.Connection = connection;
                FantasyTeam returnFantasyTeam = await GetFantasyTeamDataByIdAsync(fantasyTeamId);
                try
                {
                    await connection.OpenAsync();
                    command.ExecuteNonQuery();
                    return returnFantasyTeam;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }
        }

        public async Task<List<Guid>> GetAllTeamsWhereDriverIsPresent(Guid driverId)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            List<Guid> fantasyTeamIds = new List<Guid>();
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand($"SELECT FantasyTeam.FantasyTeamId " +
                    $"FROM FantasyTeam " +
                    $"INNER JOIN FantasyTeamToDriver ON FantasyTeam.FantasyTeamId = FantasyTeamToDriver.FantasyTeamId " +
                    $"INNER JOIN Driver ON FantasyTeamToDriver.DriverId = Driver.DriverId " +
                    $"WHERE Driver.DriverId = @driverId ");
                command.Parameters.AddWithValue("@driverId", driverId);
                command.Connection = connection;

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            fantasyTeamIds.Add(reader.GetGuid(0));
                        }
                    }
                    else
                    {
                        string.Format("Sql error {0}, number {1}", 2, 1);
                        return fantasyTeamIds;
                    }
                    return fantasyTeamIds;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }
        }
    }
}
