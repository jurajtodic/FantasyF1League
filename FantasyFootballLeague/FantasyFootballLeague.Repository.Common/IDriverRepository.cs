using FantasyFootballLeague.Common;
using FantasyFootballLeague.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Repository.Common
{
    public interface IDriverRepository
    {
        Task<List<Driver>> GetAllDriversAsync(Paging page, Sorting sort, DriverFilter filter);
        Task<Driver> GetDriverById(Guid id);
        Task<Driver> AddDriver(Driver driver);
        Task<Driver> UpdatedDriver(Guid id, Driver driver);
        Task<bool> DeleteDriver(Guid id);
        Task<bool> UpdateDriverTotalPoints(Guid id, int position, ScoringRules rules);
    }
}
