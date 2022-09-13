using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Model.Common
{
    public interface IFantasyTeam
    {
        System.Guid FantasyTeamId { get; set; }
        string FantasyTeamName { get; set; }
        string Username { get; set; }
        int FreeSubsLeft { get; set; }
        DateTime CreationDate { get; set; }
        DateTime LastUpdated { get; set; }
        System.Guid FantasyLeagueId { get; set; }
        double RemainingBudget { get; set; }
        int TotalPoints { get; set; }

    }
}
