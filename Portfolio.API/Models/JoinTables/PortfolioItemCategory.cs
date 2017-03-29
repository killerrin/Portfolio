using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Models.JoinTables
{
    public class PortfolioItemCategory
    {
        public int PortfolioItemID { get; set; }
        public PortfolioItem PortfolioItem { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
