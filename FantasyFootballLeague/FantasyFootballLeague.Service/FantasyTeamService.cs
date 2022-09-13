using FantasyFootballLeague.Common;
using FantasyFootballLeague.Model;
using FantasyFootballLeague.Repository;
using FantasyFootballLeague.Repository.Common;
using FantasyFootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Service
{
    public class FantasyTeamService : IFantasyTeamService
    {
        protected IFantasyTeamRepository FantasyTeamRepository;
        protected IDriverRepository DriverRepository;
        protected IFantasyTeamDriverRepository FantasyTeamDriverRepository;
        protected IFantasyLeagueRepository FantasyLeagueRepository;
        public FantasyTeamService(IFantasyTeamRepository fantasyTeamRepository, IDriverRepository driverRepository, IFantasyTeamDriverRepository fantasyTeamDriverRepository, IFantasyLeagueRepository fantasyLeagueRepository)
        {
            this.FantasyTeamRepository = fantasyTeamRepository;
            this.DriverRepository = driverRepository;
            this.FantasyTeamDriverRepository = fantasyTeamDriverRepository;
            this.FantasyLeagueRepository = fantasyLeagueRepository;
        }

        public async Task<List<FantasyTeam>> GetFantasyTeamDataAsync(FantasyTeamFilter filter, FantasyTeamPaging paging, FantasyTeamSorting sorting)
        {
            return await FantasyTeamRepository.GetFantasyTeamDataAsync(filter, paging, sorting);
        }
        public async Task<FantasyTeam> GetFantasyTeamDataByIdAsync(System.Guid id)
        {
            return await FantasyTeamRepository.GetFantasyTeamDataByIdAsync(id);
        }
        
        public async Task<FantasyTeam> PostFantasyTeamDataAsync(FantasyTeam fantasyTeam)
        {
            fantasyTeam.FantasyTeamId = Guid.NewGuid();
            fantasyTeam.CreationDate = DateTime.UtcNow;
            fantasyTeam.LastUpdated = DateTime.UtcNow;
            fantasyTeam.TotalPoints = 0;
            return await FantasyTeamRepository.PostFantasyTeamDataAsync(fantasyTeam);
        }

        public async Task<FantasyTeam> PutFantasyTeamDataAsync(Guid id, FantasyTeam fantasyTeam)
        {
            fantasyTeam.LastUpdated = DateTime.UtcNow;
            return await FantasyTeamRepository.PutFantasyTeamDataAsync(id, fantasyTeam);
        }

        public async Task<bool> DeleteFantasyTeamDataAsync(Guid id)
        {
            return await FantasyTeamRepository.DeleteFantasyTeamDataAsync(id);
        }
        public async Task<List<Driver>> GetDriversFromFantasyTeam(Guid fantasyTeamId)
        {
            return await FantasyTeamRepository.GetDriversFromFantasyTeam(fantasyTeamId);
        }

        public async Task<FantasyTeamDriver> AddDriverToFantasyTeam(Guid fantasyTeamId, Guid driverId)
        {
            FantasyTeam fantasyTeam = await FantasyTeamRepository.GetFantasyTeamDataByIdAsync(fantasyTeamId);
            Driver driver = await DriverRepository.GetDriverById(driverId);
            FantasyLeague fantasyLeague = await FantasyLeagueRepository.GetFantasyLeagueDataByIdAsync(fantasyTeam.FantasyLeagueId);

            int numberOfDriversInTeam = await FantasyTeamDriverRepository.GetNumberOfDriversInTeam(fantasyTeamId);

            if (fantasyTeam.RemainingBudget < driver.Price) { return null; }
            else if (numberOfDriversInTeam >= fantasyLeague.MaximumDriversPerTeam) { return null; }
            else
            {
                FantasyTeamDriver newFantasyTeamDriver = new FantasyTeamDriver(fantasyTeamId, driverId);
                await FantasyTeamRepository.UpdateFantasyTeamAfterAddingNewDriver(fantasyTeamId, driver.Price, driver.TotalPoints);
                fantasyTeam.LastUpdated = DateTime.UtcNow;
                return await FantasyTeamDriverRepository.PostFantasyTeamDriverDataAsync(newFantasyTeamDriver);
            }
        }

        public async Task<FantasyTeam> RemoveDriverFromFantasyTeam(Guid fantasyTeamId, Guid driverId)
        {
            FantasyTeam fantasyTeam = await FantasyTeamRepository.GetFantasyTeamDataByIdAsync(fantasyTeamId);
            Driver driver = await DriverRepository.GetDriverById(driverId);

            if (fantasyTeam.FreeSubsLeft <= 0) { return null;  }
            else
            {
                await FantasyTeamDriverRepository.DeleteDriverFromFantasyTeam(fantasyTeamId, driverId);
                await FantasyTeamRepository.UpdateFantasyTeamAfterDeletingDriver(fantasyTeamId, driver.Price, driver.TotalPoints);
                fantasyTeam.LastUpdated = DateTime.UtcNow;
                return await FantasyTeamRepository.GetFantasyTeamDataByIdAsync(fantasyTeamId);
            }
        }
    }
}
