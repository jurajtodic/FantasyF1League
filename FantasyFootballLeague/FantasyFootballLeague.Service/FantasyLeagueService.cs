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
    public class FantasyLeagueService : IFantasyLeagueService
    {
        protected IFantasyLeagueRepository Repository;
        public FantasyLeagueService(IFantasyLeagueRepository repository)
        {
            this.Repository = repository;
        }

        public async Task<List<FantasyLeague>> GetFantasyLeagueDataAsync(FantasyLeagueFilter filter, FantasyLeaguePaging paging, FantasyLeagueSorting sorting)
        {
            return await Repository.GetFantasyLeagueDataAsync(filter, paging, sorting);
        }

        public async Task<FantasyLeague> GetFantasyLeagueDataByIdAsync(System.Guid id)
        {
            return await Repository.GetFantasyLeagueDataByIdAsync(id);
        }
        public async Task<FantasyLeague> PostFantasyLeagueDataAsync(FantasyLeague fantasyLeague)
        {
            fantasyLeague.FantasyLeagueId = Guid.NewGuid();
            fantasyLeague.CreationDate = DateTime.UtcNow;
            fantasyLeague.LastUpdate = DateTime.UtcNow;
            return await Repository.PostFantasyLeagueDataAsync(fantasyLeague);
        }

        public async Task<bool> DeleteFantasyLeagueDataAsync(Guid id)
        {
            return await Repository.DeleteFantasyLeagueDataAsync(id);
        }

        public async Task<List<FantasyTeam>> GetFantasyTeamsFromLeague(Guid fantasyLeagueId, FantasyTeamFilter filter, FantasyTeamPaging paging, FantasyTeamSorting sorting)
        {
            return await Repository.GetFantasyTeamsFromLeague(fantasyLeagueId, filter, paging, sorting);
        }

        public async Task<FantasyTeam> AddFantasyTeamToLeague(Guid id, FantasyTeam team) 
        {
            FantasyLeague league = await Repository.GetFantasyLeagueDataByIdAsync(id);
            team.FantasyTeamId = Guid.NewGuid();
            team.CreationDate = DateTime.UtcNow;
            team.LastUpdated = DateTime.UtcNow;
            team.TotalPoints = 0;

            return await Repository.AddFantasyTeamToLeague(id, team, league);
        }
    }
}
