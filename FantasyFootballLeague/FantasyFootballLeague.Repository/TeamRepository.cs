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
    public class TeamRepository : ITeamRepository
    {
        //string connectionString = "Data Source=DESKTOP-27CEH1K\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";
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

        public async Task<Team> GetAsync(Guid id)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {                                
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Constructor WHERE ConstructorId = @guid", conn);

                try
                {
                    await conn.OpenAsync();
                    command.Parameters.AddWithValue("@guid", id);
                    var count = await command.ExecuteScalarAsync();
                    if (count == null)
                    {
                        return null;
                    }

                }

                catch (SqlException e)
                {                    
                    throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
                }
                

            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Constructor WHERE ConstructorId = @guid", conn);
                Team searchedTeam = new Team();
                try 
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@guid", id);

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            searchedTeam.Id = reader.GetGuid(0);
                            searchedTeam.Name = reader.GetString(1);                            
                        }
                    }
                    reader.Close();
                    return searchedTeam;
                }

                catch (SqlException e)
                {
                    throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
                }

            }

        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }
            List<Team> teams = new List<Team>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand("SELECT * FROM Constructor;")
                {
                    Connection = conn
                };

                try 
                {
                    await conn.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            teams.Add(new Team(reader.GetGuid(0), reader.GetString(1)));
                        }

                    }
                    else
                    {
                        return null;
                    }
                    reader.Close();
                    return teams;
                }

                catch (SqlException e)
                {
                    throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
                }


            }
        }

    }
}
