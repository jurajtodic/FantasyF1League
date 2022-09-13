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
    public class TeamService : ITeamService
    {
        protected ITeamRepository TeamRepository;
        public TeamService(ITeamRepository teamRepository)
        {
            this.TeamRepository = teamRepository;
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await TeamRepository.GetAllTeamsAsync();
        }

        public async Task<Team> GetAsync(Guid Id)
        {
            return await TeamRepository.GetAsync(Id);
        }

    }
}
