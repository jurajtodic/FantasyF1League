using FantasyFootballLeague.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Model
{
    public class FantasyLeague : IFantasyLeague
    {
        public FantasyLeague(System.Guid id, string name, double budget, int maxTeams, int maxSubs, int maxDrivers, DateTime createDate, DateTime lastUpdate)
        {
            FantasyLeagueId = id;
            FantasyLeagueName = name;
            Budget = budget;
            MaximumTeams = maxTeams;
            MaximumFreeSubs = maxSubs;
            MaximumDriversPerTeam = maxDrivers;
            CreationDate = createDate;
            LastUpdate = lastUpdate;
        }

        public System.Guid FantasyLeagueId { get; set; }
        public string FantasyLeagueName { get; set; }
        public double Budget { get; set; }
        public int MaximumTeams { get; set; }
        public int MaximumFreeSubs { get; set; }
        public int MaximumDriversPerTeam { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }

    }

}
