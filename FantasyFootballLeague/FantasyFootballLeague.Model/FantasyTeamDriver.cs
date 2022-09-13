using FantasyFootballLeague.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Model
{
    public class FantasyTeamDriver : IFantasyTeamDriver
    {
        public FantasyTeamDriver(Guid fantasyTeamId, Guid driverId)
        {
            FantasyTeamId = fantasyTeamId;
            DriverId = driverId;
        }

        public Guid FantasyTeamId { get; set; }
        public Guid DriverId { get; set; }
    }
}
