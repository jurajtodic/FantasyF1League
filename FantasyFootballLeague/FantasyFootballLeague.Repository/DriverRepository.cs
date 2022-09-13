using FantasyFootballLeague.Common;
using FantasyFootballLeague.Model;
using FantasyFootballLeague.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Repository
{

    public class DriverRepository : IDriverRepository
    {
        private string connectionString = Environment.GetEnvironmentVariable("connectionString");
        int index = 0;
        private bool FlagForConnStringAdjMenthod = false;

        public void StringAdjustmentMethod() 
        {
            if(connectionString.Length >= 80)
            {
                StringBuilder sb = new StringBuilder(connectionString);
                this.index = connectionString.IndexOf("\\");
                sb.Remove(this.index, 1);
                this.connectionString = sb.ToString();
                FlagForConnStringAdjMenthod = true;
            }
            
            
        }

        public async Task<List<Driver>> GetAllDriversAsync(Paging page, Sorting sort, DriverFilter filter)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                List<Driver> drivers = new List<Driver>();

                /*da npr. na 2. stranici pocne s 3. elementom i ide do 4. elementa jer je 2 po stranici*/
                int formula = ((page.PageNumber - 1) * page.ItemsByPage);                

                SqlCommand command = new SqlCommand();
                command.Connection = conn;

                StringBuilder stringBuilder = new StringBuilder("SELECT * FROM Driver WHERE 1=1 ");

                if (filter.TotalPoints != null)
                {
                    stringBuilder.Append("AND TotalPoints = @totalPoints ");
                    command.Parameters.AddWithValue("@totalPoints", filter.TotalPoints);
                }

                if (filter.TotalPointsIsHigher != null)
                {
                    stringBuilder.Append("AND TotalPoints > @totalPointsIsHigher ");
                    command.Parameters.AddWithValue("@totalPointsIsHigher", filter.TotalPointsIsHigher);
                }

                if (filter.TotalPointsIsLower != null)
                {
                    stringBuilder.Append("AND TotalPoints < @ageIsLower ");
                    command.Parameters.AddWithValue("@ageIsLower", filter.TotalPointsIsLower);
                }

                if (filter.TotalPrice != null)
                {
                    stringBuilder.Append("AND Price = @price ");
                    command.Parameters.AddWithValue("@price", filter.TotalPrice);
                }

                if (filter.PriceIsHigher != null)
                {
                    stringBuilder.Append("AND Price > @priceIsHigher ");
                    command.Parameters.AddWithValue("@priceIsHigher", filter.PriceIsHigher);
                }

                if (filter.PriceIsLower != null)
                {
                    stringBuilder.Append("AND Price < @priceIsLower ");
                    command.Parameters.AddWithValue("@priceIsLower", filter.PriceIsLower);
                }


                stringBuilder.Append(string.Format("ORDER BY {0} {1} ", sort.OrderBy, sort.SortBy));
                stringBuilder.Append("OFFSET @formula ROWS FETCH NEXT @itemsByPage ROWS ONLY;");

                command.Parameters.AddWithValue("@formula", formula);
                command.Parameters.AddWithValue("@itemsByPage", page.ItemsByPage);

                command.CommandText = stringBuilder.ToString();
                try
                {
                    await conn.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //id,name,surname,age,isturbo,price,totalpoints,creationdate,lastupdate,constid,scoringid
                            drivers.Add(new Driver(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3),
                                                    reader.GetBoolean(4), reader.GetDouble(5), reader.GetInt32(6), reader.GetDateTime(7),
                                                    reader.GetDateTime(8), reader.GetGuid(9), reader.GetGuid(10)));
                        }

                    }
                    else
                    {
                        return drivers;
                    }
                    reader.Close();
                    return drivers;
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
                }


            }
        }

        public async Task<Driver> GetDriverById(Guid id)
        {
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int count = 0;
                SqlCommand command = new SqlCommand("SELECT Count(*) FROM Driver WHERE DriverId = @guid", conn);

                try
                {
                    await conn.OpenAsync();
                    command.Parameters.AddWithValue("@guid", id);
                    count = (int)await command.ExecuteScalarAsync();

                    if (count == 0)
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
                SqlCommand command = new SqlCommand("SELECT * FROM Driver WHERE DriverId = @guid", conn);
                Driver searchedDriver = new Driver();
                await conn.OpenAsync();
                command.Parameters.AddWithValue("@guid", id);

                try
                {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            searchedDriver.DriverId = reader.GetGuid(0);
                            searchedDriver.DriverName = reader.GetString(1);
                            searchedDriver.DriverSurname = reader.GetString(2);
                            searchedDriver.Age = reader.GetInt32(3);
                            searchedDriver.IsTurboDriver = reader.GetBoolean(4);
                            searchedDriver.Price = reader.GetDouble(5);
                            searchedDriver.TotalPoints = reader.GetInt32(6);
                            searchedDriver.CreationDate = reader.GetDateTime(7);
                            searchedDriver.LastUpdated = reader.GetDateTime(8);
                            searchedDriver.ConstructorId = reader.GetGuid(9);
                            searchedDriver.ScoringRulesId = reader.GetGuid(10);
                        }

                    }
                    reader.Close();
                    return searchedDriver;
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
                }


            }
        }
        
        public async Task<Driver> AddDriver(Driver driver)
        {

            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {        


                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO Driver
                            VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8,
                                    @param9, @param10, @param11);";

                    cmd.Parameters.AddWithValue("@param1", driver.DriverId);
                    cmd.Parameters.AddWithValue("@param2", driver.DriverName);
                    cmd.Parameters.AddWithValue("@param3", driver.DriverSurname);
                    cmd.Parameters.AddWithValue("@param4", driver.Age);
                    cmd.Parameters.AddWithValue("@param5", driver.IsTurboDriver);
                    cmd.Parameters.AddWithValue("@param6", driver.Price);
                    cmd.Parameters.AddWithValue("@param7", driver.TotalPoints);
                    cmd.Parameters.AddWithValue("@param8", driver.CreationDate);
                    cmd.Parameters.AddWithValue("@param9", driver.LastUpdated);
                    cmd.Parameters.AddWithValue("@param10", driver.ConstructorId);
                    cmd.Parameters.AddWithValue("@param11", driver.ScoringRulesId);

                    Driver newDriver = new Driver(driver.DriverId, driver.DriverName, driver.DriverSurname
                                                  , driver.Age, driver.IsTurboDriver, driver.Price,
                                                  driver.TotalPoints, driver.CreationDate, driver.LastUpdated,
                                                  driver.ConstructorId, driver.ScoringRulesId);

                    try
                    {
                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return newDriver;
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
                    }

                }

            }
        } 

        public async Task<Driver> UpdatedDriver(Guid id, Driver driver)
        {

            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand("SELECT DriverId FROM Driver WHERE DriverId = @guid", conn);
                command.Parameters.AddWithValue("@guid", id);
                await conn.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            reader.Close();
                            cmd.Connection = conn;
                            cmd.CommandText = @"UPDATE Driver SET DriverName = @param1, DriverSurname = @param2, Age = @param3, IsTurboDriver = @param4,
                                                Price = @param5, LastUpdated= @param8, ConstructorId = @param9, ScoringRulesId = @param10 
                                                Where DriverId = @param11";
                            cmd.Parameters.AddWithValue("@param1", driver.DriverName);
                            cmd.Parameters.AddWithValue("@param2", driver.DriverSurname);
                            cmd.Parameters.AddWithValue("@param3", driver.Age);
                            cmd.Parameters.AddWithValue("@param4", driver.IsTurboDriver);
                            cmd.Parameters.AddWithValue("@param5", driver.Price);
                            cmd.Parameters.AddWithValue("@param8", driver.LastUpdated);
                            cmd.Parameters.AddWithValue("@param9", driver.ConstructorId);
                            cmd.Parameters.AddWithValue("@param10", driver.ScoringRulesId);
                            cmd.Parameters.AddWithValue("@param11", id);

                            Driver updatedDriver = new Driver(id, driver.DriverName, driver.DriverSurname, driver.Age,
                                                              driver.IsTurboDriver, driver.Price, driver.TotalPoints,
                                                              driver.CreationDate, driver.LastUpdated, driver.ConstructorId,
                                                              driver.ScoringRulesId);
                            try
                            {
                                await cmd.ExecuteNonQueryAsync();
                                return updatedDriver;
                            }

                            catch (SqlException e)
                            {
                                throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
                            }

                        }
                    }
                    else return null;
                }
            }
        }

        public async Task<bool> DeleteDriver(Guid id)
        {

            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int count = 0;
                SqlCommand command = new SqlCommand("SELECT Count(*) FROM Driver WHERE DriverId = @guid", conn);
                await conn.OpenAsync();
                command.Parameters.AddWithValue("@guid", id);

                try
                {
                    count = (int)await command.ExecuteScalarAsync();

                    if (count == 0)
                    {
                        return false;
                    }
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Driver WHERE DriverId = @guid", conn);
                command.Parameters.AddWithValue("@guid", id);

                try
                {
                    await conn.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return true;
                }
                catch (SqlException e)
                {
                    throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
                }

            }
        }
     
        public async Task<bool> UpdateDriverTotalPoints(Guid id, int position, ScoringRules rules)
        {
            
            if (FlagForConnStringAdjMenthod == false)
            {
                StringAdjustmentMethod();
            }


            int scoreToAdd = 0;

            if(position == 0) { scoreToAdd = 0; }
            else if (position == 1) { scoreToAdd = rules.PositionOne; }
            else if (position == 2) { scoreToAdd = rules.PositionTwo; }
            else if (position == 3) { scoreToAdd = rules.PositionThree; }
            else if (position == 4) { scoreToAdd = rules.PositionFour; }
            else if (position == 5) { scoreToAdd = rules.PositionFive; }
            else if (position == 6) { scoreToAdd = rules.PositionSix; }
            else if (position == 7) { scoreToAdd = rules.PositionSeven; }
            else if (position == 8) { scoreToAdd = rules.PositionEight; }
            else if (position == 9) { scoreToAdd = rules.PositionNine; }
            else if (position == 10) { scoreToAdd = rules.PositionTen; }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand("SELECT DriverId FROM Driver WHERE DriverId = @guid", conn);
                command.Parameters.AddWithValue("@guid", id);
                await conn.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            reader.Close();
                            cmd.Connection = conn;
                            cmd.CommandText = @"UPDATE Driver SET TotalPoints = TotalPoints + @newPoints,
                                                LastUpdated = CURRENT_TIMESTAMP Where DriverId = @guid";
                            cmd.Parameters.AddWithValue("@newPoints", scoreToAdd);                            
                            cmd.Parameters.AddWithValue("@guid", id);
                            
                            try
                            {
                                await cmd.ExecuteNonQueryAsync();
                                return true;
                            }

                            catch (SqlException e)
                            {
                                throw new Exception(string.Format("Sql error {0}, number {1} ", e.Message, e.Number));
                            }
                        }
                    }
                    else return false;
                }
            }

        }
    }
}
