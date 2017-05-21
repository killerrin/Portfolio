using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Models.JoinTables
{
    public class PortfolioItemTag
    {
        [JsonIgnore]
        public PortfolioItem PortfolioItem { get; set; }
        public int PortfolioItemID { get; set; }

        [JsonIgnore]
        public Tag Tag { get; set; }
        public int TagID { get; set; }
    }
}
