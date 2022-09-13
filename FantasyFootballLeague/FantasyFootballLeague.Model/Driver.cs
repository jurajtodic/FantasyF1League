using FantasyFootballLeague.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Model
{
    public class  Driver : IDriver
    {
		public Guid DriverId { get; set; }
		public string DriverName { get; set; }
		public string DriverSurname { get; set; }
		public int Age { get; set; }
		public bool IsTurboDriver { get; set; }
		public double Price { get; set; }
		public int TotalPoints { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime LastUpdated { get; set; }
		public Guid ConstructorId { get; set; }
		public Guid ScoringRulesId { get; set; }

		public Driver() { }

		public Driver(Guid driverId, string driverName, string driverSurname, int age, bool isTurboDriver, double price, int totalPoints
					 ,DateTime creationDate, DateTime lastUpdate, Guid constructionId, Guid scoringRulesId)
        {
			this.DriverId = driverId;
			this.DriverName = driverName;
			this.DriverSurname = driverSurname;
			this.Age = age;
			this.IsTurboDriver = isTurboDriver;
			this.Price = price;
			this.TotalPoints = totalPoints;
			this.CreationDate = creationDate;
			this.LastUpdated = lastUpdate;
			this.ConstructorId = constructionId;
			this.ScoringRulesId = scoringRulesId;
        }

    }
}



/*DriverId uniqueidentifier NOT NULL PRIMARY KEY,
	DriverName varchar(255) NOT NULL,
	DriverSurname varchar(255) NOT NULL,
	Age int NOT NULL,
	IsTurboDriver BIT NOT NULL,
	Price FLOAT NOT NULL,
	TotalPoints int NOT NULL,
	CreationDate DateTime NOT NULL,
	LastUpdated DateTime NOT NULL,
	ConstructorId uniqueidentifier FOREIGN KEY REFERENCES Constructor(ConstructorId),
	ScoringRulesId uniqueidentifier FOREIGN KEY REFERENCES ScoringRules(ScoringRulesId)*/