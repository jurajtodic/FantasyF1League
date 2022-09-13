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
    public class ScoringRulesRepository : IScoringRulesRepository
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


        public async Task<ScoringRules> GetScoringRulesByIdAsync(Guid id)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.ScoringRules WHERE ScoringRulesId = @guid", conn);

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
                
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.ScoringRules WHERE ScoringRulesId = @guid", conn);
                ScoringRules searchedScoringRules = new ScoringRules();


                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@guid", id);

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            searchedScoringRules.ScoringRulesId = reader.GetGuid(0);
                            searchedScoringRules.PositionOne = reader.GetInt32(1);
                            searchedScoringRules.PositionTwo = reader.GetInt32(2);
                            searchedScoringRules.PositionThree = reader.GetInt32(3);
                            searchedScoringRules.PositionFour = reader.GetInt32(4);
                            searchedScoringRules.PositionFive = reader.GetInt32(5);
                            searchedScoringRules.PositionSix = reader.GetInt32(6);
                            searchedScoringRules.PositionSeven = reader.GetInt32(7);
                            searchedScoringRules.PositionEight = reader.GetInt32(8);
                            searchedScoringRules.PositionNine = reader.GetInt32(9);
                            searchedScoringRules.PositionTen = reader.GetInt32(10);
                            searchedScoringRules.CreationDate = reader.GetDateTime(11);
                            searchedScoringRules.LastUpdated = reader.GetDateTime(12);

                        }

                    }
                    reader.Close();
                    return searchedScoringRules;
                }

                catch (SqlException e)
                {
                    throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
                }


            }


        }

        public async Task<List<ScoringRules>> GetAllScoringRulesAsync()
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.ScoringRules", conn);

                try
                {
                    await conn.OpenAsync();
                    var count = await command.ExecuteScalarAsync();
                    if (count == null)
                    {
                        conn.Close();
                        return null;
                    }
                }
                catch (SqlException e)
                {
                    conn.Close();
                    throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
                }
                conn.Close();
            }

            

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.ScoringRules", conn);
                List<ScoringRules> listScoringRules = new List<ScoringRules>();
                try
                {
                    conn.Open();

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            listScoringRules.Add(new ScoringRules(reader.GetGuid(0), reader.GetInt32(1),
                                reader.GetInt32(2),
                                reader.GetInt32(3),
                                reader.GetInt32(4),
                                reader.GetInt32(5),
                                reader.GetInt32(6),
                                reader.GetInt32(7),
                                reader.GetInt32(8),
                                reader.GetInt32(9),
                                reader.GetInt32(10),
                                reader.GetDateTime(11),
                                reader.GetDateTime(12)
                                ));
                        }
                    }
                    reader.Close();
                    return listScoringRules;
                }

                catch (SqlException e)
                {
                    throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
                }

            }
        }

        public async Task<ScoringRules> UpdateScoringRulesByIdAsync(System.Guid id, ScoringRules rules)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand("SELECT ScoringRulesId FROM ScoringRules WHERE ScoringRulesId = @guid", conn);
                command.Parameters.AddWithValue("@guid", id);
                conn.Open();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            reader.Close();
                            cmd.Connection = conn;
                            DateTime newDateTime = DateTime.UtcNow;                            
                            cmd.CommandText = @"UPDATE dbo.ScoringRules SET Position1 = @param1, Position2 = @param2,
                                Position3 = @param3, Position4 = @param4, Position5 = @param5, Position6 = @param6,
                                Position7 = @param7, Position8 = @param8, Position9 = @param9, Position10 = @param10,
                                LastUpdated = CURRENT_TIMESTAMP WHERE ScoringRulesId = @id";
                            cmd.Parameters.AddWithValue("@param1", rules.PositionOne);
                            cmd.Parameters.AddWithValue("@param2", rules.PositionTwo);
                            cmd.Parameters.AddWithValue("@param3", rules.PositionThree);
                            cmd.Parameters.AddWithValue("@param4", rules.PositionFour);
                            cmd.Parameters.AddWithValue("@param5", rules.PositionFive);
                            cmd.Parameters.AddWithValue("@param6", rules.PositionSix);
                            cmd.Parameters.AddWithValue("@param7", rules.PositionSeven);
                            cmd.Parameters.AddWithValue("@param8", rules.PositionEight);
                            cmd.Parameters.AddWithValue("@param9", rules.PositionNine);
                            cmd.Parameters.AddWithValue("@param10", rules.PositionTen);
                            //cmd.Parameters.AddWithValue("@lastUpdated", newDateTime);
                            cmd.Parameters.AddWithValue("@id", id);

                            ScoringRules updatedScoringRules = new ScoringRules(id, rules.PositionOne, rules.PositionTwo, rules.PositionThree,
                                                                                 rules.PositionFour, rules.PositionFive, rules.PositionSix,
                                                                                 rules.PositionSeven, rules.PositionEight, rules.PositionNine,
                                                                                 rules.PositionTen, newDateTime);
                            try 
                            {
                                await cmd.ExecuteNonQueryAsync();
                                return updatedScoringRules;
                            }
                            catch (SqlException e)
                            {
                                throw new Exception(string.Format("sql error {0}, number {1} ", e.Message, e.Number));
                            }

                        }
                    }
                    else return null;
                }


            }
        }

    }

}
