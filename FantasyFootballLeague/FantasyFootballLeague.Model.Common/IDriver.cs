using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Model.Common
{
    public interface IDriver
    {
		 Guid DriverId { get; set; }
		 string DriverName { get; set; }
		 string DriverSurname { get; set; }
		 int Age { get; set; }
		 bool IsTurboDriver { get; set; }
		 double Price { get; set; }
		 int TotalPoints { get; set; }
		 DateTime CreationDate { get; set; }
		 DateTime LastUpdated { get; set; }
		 Guid ConstructorId { get; set; }
		 Guid ScoringRulesId { get; set; }
	}
}
