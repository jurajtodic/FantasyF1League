using FantasyFootballLeague.Common;
using FantasyFootballLeague.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Service.Common
{
    public interface IFantasyLeagueService
    {
        Task<List<FantasyLeague>> GetFantasyLeagueDataAsync(FantasyLeagueFilter filter, FantasyLeaguePaging paging, FantasyLeagueSorting sorting);
        Task<FantasyLeague> GetFantasyLeagueDataByIdAsync(System.Guid id);
        Task<FantasyLeague> PostFantasyLeagueDataAsync(FantasyLeague fantasyLeague);
        Task<bool> DeleteFantasyLeagueDataAsync(Guid id);
        Task<List<FantasyTeam>> GetFantasyTeamsFromLeague(Guid fantasyLeagueId, FantasyTeamFilter filter, FantasyTeamPaging paging, FantasyTeamSorting sorting);
        Task<FantasyTeam> AddFantasyTeamToLeague(Guid id, FantasyTeam team);

    }
}
