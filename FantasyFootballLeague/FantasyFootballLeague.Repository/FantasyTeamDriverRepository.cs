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
    public class FantasyTeamDriverRepository : IFantasyTeamDriverRepository
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

        public async Task<List<FantasyTeamDriver>> GetFantasyTeamDriversAsync()
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            List<FantasyTeamDriver> fantasyTeamDriverList = new List<FantasyTeamDriver>();
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM FantasyTeamToDriver ");
                command.Connection = connection;

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            fantasyTeamDriverList.Add(new FantasyTeamDriver(reader.GetGuid(0), reader.GetGuid(1)));
                        }
                    }
                    else
                    {
                        string.Format("Sql error {0}, number {1}", 2, 1);

                        return null;
                    }
                    return fantasyTeamDriverList;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }
        }

        public async Task<FantasyTeamDriver> PostFantasyTeamDriverDataAsync(FantasyTeamDriver fantasyTeamDriver)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand("INSERT INTO FantasyTeamToDriver VALUES ((SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamId=@fantasyTeamId),(SELECT DriverId FROM Driver WHERE DriverId=@driverId)) ", connection);
                command.Parameters.AddWithValue("@fantasyTeamId", fantasyTeamDriver.FantasyTeamId);
                command.Parameters.AddWithValue("@driverId", fantasyTeamDriver.DriverId);


                command.Connection = connection;
                FantasyTeamDriver returnFantasyTeamDriver = new FantasyTeamDriver(fantasyTeamDriver.FantasyTeamId, fantasyTeamDriver.DriverId);
                try
                {
                    await connection.OpenAsync();
                    command.ExecuteNonQuery();
                    return returnFantasyTeamDriver;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }
        }

        public async Task<bool> DeleteFantasyTeamDriverDataAsync(Guid driverId)   
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT DriverId FROM FantasyTeamToDriver WHERE DriverId = @driverId ", connection);
                command.Parameters.AddWithValue("@driverId", driverId);
                connection.Open();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            reader.Close();
                            cmd.Connection = connection;

                            cmd.CommandText = "DELETE FROM FantasyTeamToDriver WHERE DriverId = @id ";
                            cmd.Parameters.AddWithValue("@id", driverId);
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

        public async Task<int> GetNumberOfDriversInTeam(Guid fantasyTeamId)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            int numberOfDriversInTeam = 0;
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM FantasyTeamToDriver WHERE FantasyTeamToDriver.FantasyTeamId = @fantasyTeamId ");
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
                            numberOfDriversInTeam = reader.GetInt32(0);
                        }
                    }
                    else
                    {
                        string.Format("Sql error {0}, number {1}", 2, 1);
                        return numberOfDriversInTeam=0;
                    }
                    return numberOfDriversInTeam;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }

            }
        }

        public async Task<bool> DeleteDriverFromFantasyTeam(Guid fantasyTeamId, Guid driverId)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand("DELETE FROM FantasyTeamToDriver WHERE DriverId = @driverId AND FantasyTeamId=@fantasyTeamId ", connection);
                command.Parameters.AddWithValue("@fantasyTeamId", fantasyTeamId);
                command.Parameters.AddWithValue("@driverId", driverId);

                command.Connection = connection;
                try
                {
                    await connection.OpenAsync();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception exc)
                {
                    throw new Exception(string.Format("Sql error {0} ", exc.Message));
                }
            }
        }
    }
}
