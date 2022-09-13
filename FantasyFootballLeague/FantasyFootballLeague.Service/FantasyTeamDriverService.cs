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
    public class FantasyTeamDriverService : IFantasyTeamDriverService
    {
        protected IFantasyTeamDriverRepository Repository;
        public FantasyTeamDriverService(IFantasyTeamDriverRepository repository, IFantasyTeamRepository fantasyTeamRepository)
        {
            this.Repository = repository;
        }

        public async Task<List<FantasyTeamDriver>> GetFantasyTeamDriversAsync()
        {
            return await Repository.GetFantasyTeamDriversAsync();
        }

        public async Task<FantasyTeamDriver> PostFantasyTeamDriverDataAsync(FantasyTeamDriver fantasyTeamDriver)
        {
            return await Repository.PostFantasyTeamDriverDataAsync(fantasyTeamDriver);

        }

        public async Task<bool> DeleteFantasyTeamDriverDataAsync(Guid id)
        {
            return await Repository.DeleteFantasyTeamDriverDataAsync(id);
        }

        public async Task<int> GetNumberOfDriversInTeam(Guid fantasyTeamId)
        {
            return await Repository.GetNumberOfDriversInTeam(fantasyTeamId);
        }


    }
}
