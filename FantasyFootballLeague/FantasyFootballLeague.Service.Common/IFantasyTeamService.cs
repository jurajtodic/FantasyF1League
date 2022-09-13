using FantasyFootballLeague.Common;
using FantasyFootballLeague.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Service.Common
{
    public interface IFantasyTeamService
    {
        Task<List<FantasyTeam>> GetFantasyTeamDataAsync(FantasyTeamFilter filter, FantasyTeamPaging paging, FantasyTeamSorting sorting);
        Task<FantasyTeam> GetFantasyTeamDataByIdAsync(System.Guid id);
        Task<FantasyTeam> PostFantasyTeamDataAsync(FantasyTeam fantasyTeam);
        Task<FantasyTeam> PutFantasyTeamDataAsync(Guid id, FantasyTeam fantasyTeam);
        Task<bool> DeleteFantasyTeamDataAsync(Guid id);
        Task<List<Driver>> GetDriversFromFantasyTeam(Guid fantasyTeamId);
        Task<FantasyTeamDriver> AddDriverToFantasyTeam(Guid fantasyTeamId, Guid driverId);
        Task<FantasyTeam> RemoveDriverFromFantasyTeam(Guid fantasyTeamId, Guid driverId);





    }
}
