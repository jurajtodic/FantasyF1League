using FantasyFootballLeague.Model;
using FantasyFootballLeague.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FantasyFootballLeague.Common;

namespace FantasyFootballLeague.Repository
{
    public class FantasyLeagueRepository : IFantasyLeagueRepository
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

        public async Task<List<FantasyLeague>> GetFantasyLeagueDataAsync(FantasyLeagueFilter filter, FantasyLeaguePaging paging, FantasyLeagueSorting sorting)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            List<FantasyLeague> fantasyLeagueList = new List<FantasyLeague>();
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                StringBuilder stringBuilder = new StringBuilder("SELECT * FROM FantasyLeague ", 500);
                if (filter != null)
                {
                    stringBuilder.Append("WHERE 1=1 ");

                    // FILTER - searchstring
                    if (filter.SearchString != null)
                    {
                        stringBuilder.AppendFormat("AND FantasyLeagueName LIKE '%{0}%' ", filter.SearchString);
                    }
                    // FILTER - BUDGET
                    if (filter.LowerBudget != null && filter.HigherBudget != null)
                    {
                        stringBuilder.AppendFormat("AND Budget BETWEEN {0} AND {1} ", filter.LowerBudget, filter.HigherBudget);
                    }
                    else
                    {
                        if (filter.HigherBudget != null)
                        {
                            stringBuilder.AppendFormat("AND Budget > {0} ", filter.HigherBudget);
                        }
                        if (filter.LowerBudget != null)
                        {
                            stringBuilder.AppendFormat("AND Budget < {0} ", filter.LowerBudget);
                        }
                        // FILTER - FREESUBS
                        if (filter.HigherFreeSubs != null)
                        {
                            stringBuilder.AppendFormat("AND MaximumFreeSubs > {0} ", filter.HigherFreeSubs);
                        }
                        if (filter.LowerFreeSubs != null)
                        {
                            stringBuilder.AppendFormat("AND MaximumFreeSubs < {0} ", filter.LowerFreeSubs);
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
                            fantasyLeagueList.Add(new FantasyLeague(
                                reader.GetGuid(0), reader.GetString(1), reader.GetDouble(2), reader.GetInt32(3),
                                reader.GetInt32(4), reader.GetInt32(5), reader.GetDateTime(6), reader.GetDateTime(7))
                                );
                        }
                    }
                    else
                    {
                        string.Format("Sql error {0}, number {1}", 2,1);
                        return fantasyLeagueList;
                    }
                    return fantasyLeagueList;
                }
                catch(Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }
        }
        
        public async Task<FantasyLeague> GetFantasyLeagueDataByIdAsync(System.Guid id)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            SqlConnection connection = new SqlConnection(connectionString);
            List<FantasyLeague> fantasyLeagueList = new List<FantasyLeague>();

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM FantasyLeague WHERE FantasyLeagueId = @id ", connection);
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
                            fantasyLeagueList.Add(new FantasyLeague(
                                reader.GetGuid(0), reader.GetString(1), reader.GetDouble(2), reader.GetInt32(3),
                                reader.GetInt32(4), reader.GetInt32(5), reader.GetDateTime(6), reader.GetDateTime(7))
                                );
                        }
                    }
                    else
                    {
                        string.Format("Sql error {0}, number {1}", 2, 1);
                        return null;
                    }
                    return fantasyLeagueList[0];
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }

            }
        }

        public async Task<FantasyLeague> PostFantasyLeagueDataAsync (FantasyLeague fantasyLeague)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            SqlConnection connection = new SqlConnection(connectionString);
            
            using (connection)
            {
                SqlCommand command = new SqlCommand("INSERT INTO FantasyLeague VALUES (@id, @name, @budget, @maxTeams, @maxFreeSubs, @maxDrivers, @creationDate, @lastUpdated) ", connection);
                command.Parameters.AddWithValue("@id", fantasyLeague.FantasyLeagueId);
                command.Parameters.AddWithValue("@name", fantasyLeague.FantasyLeagueName);
                command.Parameters.AddWithValue("@budget", fantasyLeague.Budget);
                command.Parameters.AddWithValue("@maxTeams", fantasyLeague.MaximumTeams);
                command.Parameters.AddWithValue("@maxFreeSubs", fantasyLeague.MaximumFreeSubs);
                command.Parameters.AddWithValue("@maxDrivers", fantasyLeague.MaximumDriversPerTeam);
                command.Parameters.AddWithValue("@creationDate", fantasyLeague.CreationDate);
                command.Parameters.AddWithValue("@lastUpdated", fantasyLeague.LastUpdate);

                command.Connection = connection;
                FantasyLeague returnFantasyLeague = new FantasyLeague(fantasyLeague.FantasyLeagueId, fantasyLeague.FantasyLeagueName, fantasyLeague.Budget, fantasyLeague.MaximumTeams, fantasyLeague.MaximumFreeSubs, fantasyLeague.MaximumDriversPerTeam, fantasyLeague.CreationDate, fantasyLeague.LastUpdate);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return returnFantasyLeague;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }
        }

        public async Task<bool> DeleteFantasyLeagueDataAsync(Guid id)
        {

            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT FantasyLeagueId FROM FantasyLeague WHERE FantasyLeagueId = @fLeagueid ", connection);
                command.Parameters.AddWithValue("@fLeagueid", id);
                connection.Open();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            reader.Close();
                            cmd.Connection = connection;

                            cmd.CommandText = "DELETE FROM FantasyLeague WHERE FantasyLeagueId = @id ";
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

        public async Task<List<FantasyTeam>> GetFantasyTeamsFromLeague(Guid fantasyLeagueId, FantasyTeamFilter filter, FantasyTeamPaging paging, FantasyTeamSorting sorting)
        {

            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            List<FantasyTeam> fantasyTeams = new List<FantasyTeam>();
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                StringBuilder stringBuilder = new StringBuilder($"SELECT FantasyTeam.FantasyTeamId, FantasyTeamName, " +
                    $"Username, FreeSubsLeft, FantasyTeam.CreationDate, FantasyTeam.LastUpdated, " +
                    $"FantasyTeam.FantasyLeagueId, RemainingBudget, TotalPoints " +
                    $"FROM FantasyLeague " +
                    $"INNER JOIN FantasyTeam ON FantasyLeague.FantasyLeagueId = FantasyTeam.FantasyLeagueId " +
                    $"WHERE FantasyLeague.FantasyLeagueId = @fantasyLeagueId ", 1000);

                if (filter != null)
                {
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
                command.Parameters.AddWithValue("@fantasyLeagueId", fantasyLeagueId);
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
                            fantasyTeams.Add(new FantasyTeam(
                                reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3),
                                reader.GetDateTime(4), reader.GetDateTime(5), reader.GetGuid(6), reader.GetDouble(7), reader.GetInt32(8))
                                );
                        }
                    }
                    else
                    {
                        string.Format("Sql error {0}, number {1}", 2, 1);
                        return fantasyTeams;
                    }
                    return fantasyTeams;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }

        }

        public async Task<FantasyTeam> AddFantasyTeamToLeague(Guid id, FantasyTeam team, FantasyLeague league)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand(@"INSERT INTO FantasyTeam VALUES (@id, @name, @username, 
                                                      @freeSubs,@creationDate, @lastUpdated, @fantasyLeagueId,
                                                      @budget,@totalPoints) ", connection);
                command.Parameters.AddWithValue("@id", team.FantasyTeamId);
                command.Parameters.AddWithValue("@name", team.FantasyTeamName);
                command.Parameters.AddWithValue("@username", team.Username);
                command.Parameters.AddWithValue("@freeSubs", league.MaximumFreeSubs);                
                command.Parameters.AddWithValue("@creationDate", team.CreationDate);
                command.Parameters.AddWithValue("@lastUpdated", team.LastUpdated);
                command.Parameters.AddWithValue("@fantasyLeagueId", id);
                command.Parameters.AddWithValue("@budget", league.Budget);
                command.Parameters.AddWithValue("@totalPoints", team.TotalPoints);

                command.Connection = connection;
                FantasyTeam returnFantasyTeam = new FantasyTeam(team.FantasyTeamId,team.FantasyTeamName,team.Username,league.MaximumFreeSubs,
                                                                 team.CreationDate,team.LastUpdated, id, league.Budget, team.TotalPoints);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return returnFantasyTeam;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }

        }

    }
}
