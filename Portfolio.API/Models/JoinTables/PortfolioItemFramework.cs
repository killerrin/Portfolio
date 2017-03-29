using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Models.JoinTables
{
    public class PortfolioItemFramework
    {
        public int PortfolioItemID { get; set; }
        public PortfolioItem PortfolioItem { get; set; }

        public int FrameworkID { get; set; }
        public Framework Framework { get; set; }
    }
}
