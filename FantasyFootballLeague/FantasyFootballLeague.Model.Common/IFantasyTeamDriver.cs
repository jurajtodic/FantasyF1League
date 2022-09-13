using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Model.Common
{
    public interface IFantasyTeamDriver
    {
        Guid FantasyTeamId { get; set; }
        Guid DriverId { get; set; }
    }
}
