using FantasyFootballLeague.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Model
{
    public class ScoringRules : IScoringRules
    {
        public Guid ScoringRulesId { get; set; }
        public int PositionOne { get; set; }
        public int PositionTwo { get; set; }
        public int PositionThree { get; set; }
        public int PositionFour { get; set; }
        public int PositionFive { get; set; }
        public int PositionSix { get; set; }
        public int PositionSeven { get; set; }
        public int PositionEight { get; set; }
        public int PositionNine { get; set; }
        public int PositionTen { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdated { get; set; }

        public ScoringRules(Guid scoringRulesId, int positionone, int positiontwo, int positionthree, int positionfour, int positionfive, int positionsix,
            int positionseven, int positioneight, int positionnine, int positionten, DateTime creationDate, DateTime lastUpdated)
        {
            this.PositionOne = positionone;
            this.PositionTwo = positiontwo;
            this.PositionThree = positionthree;
            this.PositionFour = positionfour;
            this.PositionFive = positionfive;
            this.PositionSix = positionsix;
            this.PositionSeven = positionseven;
            this.PositionEight = positioneight;
            this.PositionNine = positionnine;
            this.PositionTen = positionten;
            this.ScoringRulesId = scoringRulesId;
            this.CreationDate = creationDate;
            this.LastUpdated = lastUpdated;
        }

        public ScoringRules(Guid scoringRulesId, int positionone, int positiontwo, int positionthree, int positionfour, int positionfive, int positionsix,
            int positionseven, int positioneight, int positionnine, int positionten, DateTime lastUpdated)
        {
            this.PositionOne = positionone;
            this.PositionTwo = positiontwo;
            this.PositionThree = positionthree;
            this.PositionFour = positionfour;
            this.PositionFive = positionfive;
            this.PositionSix = positionsix;
            this.PositionSeven = positionseven;
            this.PositionEight = positioneight;
            this.PositionNine = positionnine;
            this.PositionTen = positionten;
            this.ScoringRulesId = scoringRulesId;           
            this.LastUpdated = lastUpdated;
        }

        public ScoringRules()
        {

        }

    }
}

/*ScoringRulesId uniqueidentifier NOT NULL PRIMARY KEY,
Position1 int NOT NULL,
Position2 int NOT NULL,
Position3 int NOT NULL,
Position4 int NOT NULL,
Position5 int NOT NULL,
Position6 int NOT NULL,
Position7 int NOT NULL,
Position8 int NOT NULL,
Position9 int NOT NULL,
Position10 int NOT NULL,
CreationDate DateTime NOT NULL,
LastUpdated DateTime NOT NULL*/