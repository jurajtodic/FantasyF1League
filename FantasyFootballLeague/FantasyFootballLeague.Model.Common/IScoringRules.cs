using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Model.Common
{
    public interface IScoringRules
    {
         Guid ScoringRulesId { get; set; }
         int PositionOne { get; set; }
         int PositionTwo { get; set; }
         int PositionThree { get; set; }
         int PositionFour { get; set; }
         int PositionFive { get; set; }
         int PositionSix { get; set; }
         int PositionSeven { get; set; }
         int PositionEight { get; set; }
         int PositionNine { get; set; }
         int PositionTen { get; set; }
         DateTime CreationDate { get; set; }
         DateTime LastUpdated { get; set; }
    }
}
