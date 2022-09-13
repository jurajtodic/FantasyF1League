using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Common
{
    public class DriverFilter
    {
        public int? TotalPoints { get; set; }
        public int? TotalPointsIsHigher { get; set; }
        public int? TotalPointsIsLower { get; set; }
        public double? TotalPrice { get; set; }
        public double? PriceIsHigher { get; set; }
        public double?  PriceIsLower { get; set; }

        public DriverFilter(int? totalPoints, int? totalPointsIsHigher,int? totalPointsIsLower, double? totalPrice, double? priceIsHigher, double? priceIsLower)
        {
            this.TotalPoints = totalPoints;
            this.TotalPointsIsHigher = totalPointsIsHigher;
            this.TotalPointsIsLower = TotalPointsIsLower;
            this.TotalPrice = totalPrice;
            this.PriceIsHigher = priceIsHigher;
            this.PriceIsLower = priceIsLower;
        }       
    }
}
