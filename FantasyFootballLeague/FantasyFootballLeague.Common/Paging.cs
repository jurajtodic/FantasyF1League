using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballLeague.Common
{
    public class Paging
    {
        public int PageNumber { get; set; }
        public int ItemsByPage { get; set; }

        public Paging(int pageNumber, int itemsByPage)
        {
            this.PageNumber = pageNumber;
            this.ItemsByPage = itemsByPage;
        }

        public Paging()
        {

        }
    }
}
