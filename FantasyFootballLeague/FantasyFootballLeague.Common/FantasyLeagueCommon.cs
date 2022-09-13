using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Common
{
    public class FantasyLeagueFilter
    {
        public FantasyLeagueFilter(string searchstring, double? lowerbudget, double? higherbudget, int? lowerfreesubs, int? higherfreesubs)
        {
            SearchString = searchstring;
            LowerBudget = lowerbudget;
            HigherBudget = higherbudget;
            LowerFreeSubs = lowerfreesubs;
            HigherFreeSubs = higherfreesubs;
        }

        public string SearchString { get; set; }
        public double? LowerBudget { get; set; }
        public double? HigherBudget { get; set; }
        public int? LowerFreeSubs { get; set; }
        public int? HigherFreeSubs { get; set; }
    }

    public class FantasyLeagueSorting
    {
        public FantasyLeagueSorting(string orderby, string sortorder)
        {
            OrderBy = orderby;
            SortOrder = sortorder;
        }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }
    }

    public class FantasyLeaguePaging
    {
        public FantasyLeaguePaging(int rpp, int pagenum)
        {
            Rpp = rpp;
            PageNumber = pagenum;
        }
        public int Rpp { get; set; }
        public int PageNumber { get; set; }
    }


}
