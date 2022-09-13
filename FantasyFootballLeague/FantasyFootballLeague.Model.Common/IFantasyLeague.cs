using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Model.Common
{
    public interface IFantasyLeague
    {
        System.Guid FantasyLeagueId { get; set; }
        string FantasyLeagueName { get; set; }
        double Budget { get; set; }
        int MaximumTeams { get; set; }
        int MaximumFreeSubs { get; set; }
        int MaximumDriversPerTeam { get; set; }
        DateTime CreationDate { get; set; }
        DateTime LastUpdate { get; set; }
    }
}
