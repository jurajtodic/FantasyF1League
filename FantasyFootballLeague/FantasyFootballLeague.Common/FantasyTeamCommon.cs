using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Common
{
    public class FantasyTeamFilter
    {
        public FantasyTeamFilter(string searchstring, int? lowerTotalPoints, int? higherTotalPoints)
        {
            SearchString = searchstring;
            LowerTotalPoints = lowerTotalPoints;
            HigherTotalPoints = higherTotalPoints;
        }
        public string SearchString { get; set; }
        public int? LowerTotalPoints { get; set; }
        public int? HigherTotalPoints { get; set; }
    }

    public class FantasyTeamSorting
    {
        public FantasyTeamSorting(string orderBy, string sortOrder)
        {
            OrderBy = orderBy;
            SortOrder = sortOrder;
        }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }
    }

    public class FantasyTeamPaging
    {
        public FantasyTeamPaging (int rpp, int pagenum)
        {
            Rpp = rpp;
            PageNumber = pagenum;
        }
        public int Rpp { get; set; }
        public int PageNumber { get; set; }
    }

}
