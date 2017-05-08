using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Models.JoinTables
{
    public class PortfolioItemProgLanguage
    {
        public int PortfolioItemID { get; set; }
        [JsonIgnore]
        public PortfolioItem PortfolioItem { get; set; }

        public int ProgrammingLanguageID { get; set; }
        [JsonIgnore]
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
    }
}
