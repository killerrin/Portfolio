using Portfolio.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Repositories
{
    public class KeywordRepository : RepositoryBase<Keyword>
    {
        public KeywordRepository(PortfolioContext context)
            : base(context)
        {
            //Add(new Keyword { Name = "Multiplayer" });
            //Add(new Keyword { Name = "Async" });
            //Add(new Keyword { Name = "OOP" });
            //Add(new Keyword { Name = "Json" });
            //Add(new Keyword { Name = "XML" });
        }
    }
}
