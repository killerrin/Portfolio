using Portfolio.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Repositories
{
    public class FrameworkRepository : RepositoryBase<Framework>
    {
        public FrameworkRepository(PortfolioContext context)
            : base(context)
        {
            Add(new Framework { Name = "ASP.Net" });
            Add(new Framework { Name = "Web API" });
            Add(new Framework { Name = "JQuery" });
            Add(new Framework { Name = "Json.NET" });
            Add(new Framework { Name = "Monogame" });
        }
    }
}
