using Portfolio.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Repositories
{
    public class ProgrammingLanguageRepository : RepositoryBase<ProgrammingLanguage>
    {
        public ProgrammingLanguageRepository(PortfolioContext context)
            : base(context)
        {
            Add(new ProgrammingLanguage { Name = "C#" });
            Add(new ProgrammingLanguage { Name = "C++" });
            Add(new ProgrammingLanguage { Name = "JavaScript" });
        }
    }
}
