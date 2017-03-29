using Portfolio.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        public CategoryRepository(PortfolioContext context)
            : base(context)
        {
            Add(new Category { Name = "Game" });
            Add(new Category { Name = "Software" });
            Add(new Category { Name = "Website" });
            Add(new Category { Name = "API/Framework" });
            Add(new Category { Name = "Sandbox" });
            Add(new Category { Name = "Other" });
        }
    }
}
