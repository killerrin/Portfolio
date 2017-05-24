using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Models.JoinTables
{
    public class PortfolioItemRelatedItem
    {
        [JsonIgnore]
        public PortfolioItem PortfolioItem { get; set; }
        public int PortfolioItemID { get; set; }

        [JsonIgnore]
        public RelatedItem RelatedItem { get; set; }
        public int RelatedItemID { get; set; }
    }
}
