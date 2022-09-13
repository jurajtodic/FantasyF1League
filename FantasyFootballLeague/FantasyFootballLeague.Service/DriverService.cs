using FantasyFootballLeague.Common;
using FantasyFootballLeague.Model;
using FantasyFootballLeague.Repository.Common;
using FantasyFootballLeague.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Service
{
    public class DriverService : IDriverService
    {
        protected IDriverRepository DriverRepository;
        protected IScoringRulesRepository ScoringRulesRepository;
        protected IFantasyTeamRepository FantasyTeamRepository;
        protected IFantasyTeamDriverRepository FantasyTeamDriverRepository;
        protected IFantasyTeamService FantasyTeamService;
        public DriverService(IDriverRepository driverRepository, IScoringRulesRepository scoringRulesRepository, IFantasyTeamRepository fantasyTeamRepository, IFantasyTeamDriverRepository fantasyTeamDriverRepository, IFantasyTeamService fantasyTeamService)
        {
            this.DriverRepository = driverRepository;
            this.ScoringRulesRepository = scoringRulesRepository;
            this.FantasyTeamRepository = fantasyTeamRepository;
            this.FantasyTeamDriverRepository = fantasyTeamDriverRepository;
            this.FantasyTeamService = fantasyTeamService;
        }

        public async Task<List<Driver>> GetAllDriversAsync(Paging page, Sorting sort, DriverFilter filter)
        {
            return await DriverRepository.GetAllDriversAsync(page, sort, filter);
        }

        public async Task<Driver> GetDriverById(Guid id)
        {
            return await DriverRepository.GetDriverById(id);
        }

        public async Task<Driver> AddDriver(Driver driver)
        {
            driver.DriverId = Guid.NewGuid();
            driver.CreationDate = DateTime.UtcNow;
            driver.LastUpdated = DateTime.UtcNow;
            driver.TotalPoints = 0;

            return await DriverRepository.AddDriver(driver);

        }

        public async Task<Driver> UpdateDriver(Guid id, Driver driver)
        {
            driver.LastUpdated = DateTime.UtcNow;
            //driver.CreationDate = DateTime.UtcNow;
            return await DriverRepository.UpdatedDriver(id, driver);
        }

        public async Task<bool> DeleteDriver(Guid id)
        {
            return await DriverRepository.DeleteDriver(id);
        }

        public async Task<bool> UpdateDriverTotalPoints(Guid id, int position)
        {
            if(position > 10 || position < 1) { position = 0; }
            Driver newDriver = await DriverRepository.GetDriverById(id);
            ScoringRules newScoringRules = await ScoringRulesRepository.GetScoringRulesByIdAsync(newDriver.ScoringRulesId);

            // lista id-ova timova u kojima je driver
            List<Guid> teamIdsWhereDriverIsPresent = await FantasyTeamRepository.GetAllTeamsWhereDriverIsPresent(id);
            
            // delete drivera iz svih timova
            await FantasyTeamDriverRepository.DeleteFantasyTeamDriverDataAsync(id);

            // smanjivanje pointova u svakom timu gdje je izbrisan i puni refund
            foreach (Guid teamId in teamIdsWhereDriverIsPresent)
            {
                await FantasyTeamRepository.UpdateFantasyTeamAfterChangingDriverPoints(teamId, newDriver.Price ,newDriver.TotalPoints);
            }

            // update driver points
            bool result = await DriverRepository.UpdateDriverTotalPoints(id, position, newScoringRules);
            
            // ponovo vratiti drivera u svaki tim u kojem je bio
            foreach(Guid teamId in teamIdsWhereDriverIsPresent)
            {
                await FantasyTeamService.AddDriverToFantasyTeam(teamId, id);
            }

            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
