using FantasyFootballLeague.Common;
using FantasyFootballLeague.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Repository.Common
{
    public interface IFantasyTeamRepository
    {
        Task<List<FantasyTeam>> GetFantasyTeamDataAsync(FantasyTeamFilter filter, FantasyTeamPaging paging, FantasyTeamSorting sorting);
        Task<FantasyTeam> GetFantasyTeamDataByIdAsync(System.Guid id);
        Task<FantasyTeam> PostFantasyTeamDataAsync(FantasyTeam fantasyTeam);
        Task<FantasyTeam> PutFantasyTeamDataAsync(Guid id, FantasyTeam fantasyTeam);
        Task<bool> DeleteFantasyTeamDataAsync(Guid id);
        Task<List<Driver>> GetDriversFromFantasyTeam(Guid fantasyTeamId);
        Task<FantasyTeam> UpdateFantasyTeamAfterAddingNewDriver(Guid fantasyTeamId, double driverPrice, int totalDriverPoints);
        Task<FantasyTeam> UpdateFantasyTeamAfterDeletingDriver(Guid fantasyTeamId, double driverPrice, int totalDriverPoints);
        Task<List<Guid>> GetAllTeamsWhereDriverIsPresent(Guid driverId);
        Task<FantasyTeam> UpdateFantasyTeamAfterChangingDriverPoints(Guid fantasyTeamId, double driverPrice, int driverTotalPoints);








    }
}
