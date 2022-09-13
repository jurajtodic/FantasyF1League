using FantasyFootballLeague.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Repository.Common
{
    public interface IFantasyTeamDriverRepository
    {
        Task<List<FantasyTeamDriver>> GetFantasyTeamDriversAsync();
        Task<FantasyTeamDriver> PostFantasyTeamDriverDataAsync(FantasyTeamDriver fantasyTeamDriver);
        Task<bool> DeleteFantasyTeamDriverDataAsync(Guid id);
        Task<int> GetNumberOfDriversInTeam(Guid fantasyTeamId);
        Task<bool> DeleteDriverFromFantasyTeam(Guid fantasyTeamId, Guid driverId);



    }
}
