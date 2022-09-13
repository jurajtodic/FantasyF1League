using FantasyFootballLeague.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Service.Common
{
    public interface ITeamService
    {
        Task<List<Team>> GetAllTeamsAsync();
        Task<Team> GetAsync(Guid Id);
    }
}
