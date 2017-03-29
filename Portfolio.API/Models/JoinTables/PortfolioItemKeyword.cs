using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Models.JoinTables
{
    public class PortfolioItemKeyword
    {
        public int PortfolioItemID { get; set; }
        public PortfolioItem PortfolioItem { get; set; }

        public int KeywordID { get; set; }
        public Keyword Keyword { get; set; }
    }
}
