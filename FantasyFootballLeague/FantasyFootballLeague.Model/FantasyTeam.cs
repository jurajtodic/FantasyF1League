using FantasyFootballLeague.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Model
{
    public class FantasyTeam : IFantasyTeam
    {
        public FantasyTeam(System.Guid fantasyTeamId, string name, string username, int freeSubsLeft, DateTime createDate, DateTime lastUpdate, Guid fantasyLeagueId, double remainingBudget, int totalPoints)
        {
            FantasyTeamId = fantasyTeamId;
            FantasyTeamName = name;
            Username = username;
            FreeSubsLeft = freeSubsLeft;
            CreationDate = createDate;
            LastUpdated = lastUpdate;
            FantasyLeagueId = fantasyLeagueId;
            RemainingBudget = remainingBudget;
            TotalPoints = totalPoints;
        }

        public FantasyTeam() 
        { 
        }

        public System.Guid FantasyTeamId { get; set; }
        public string FantasyTeamName { get; set; }
        public string Username { get; set; }
        public int FreeSubsLeft { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public System.Guid FantasyLeagueId { get; set; }
        public double RemainingBudget { get; set; }
        public int TotalPoints { get; set; }

    }
}
