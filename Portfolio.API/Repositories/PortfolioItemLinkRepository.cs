using Portfolio.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Repositories
{
    public class PortfolioItemLinkRepository : RepositoryBase<PortfolioItemLink>
    {
        public PortfolioItemLinkRepository(PortfolioContext context)
            : base(context)
        {

        }
    }
}
