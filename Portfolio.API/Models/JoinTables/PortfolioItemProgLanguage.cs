using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Models.JoinTables
{
    public class PortfolioItemProgLanguage
    {
        public int PortfolioItemID { get; set; }
        public PortfolioItem PortfolioItem { get; set; }

        public int ProgrammingLanguageID { get; set; }
        public ProgrammingLanguage ProgrammingLanguage { get; set; }
    }
}
