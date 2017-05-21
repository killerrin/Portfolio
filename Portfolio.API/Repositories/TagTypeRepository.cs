using Portfolio.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Repositories
{
    public class TagTypeRepository : RepositoryBase<TagType>
    {
        public TagTypeRepository(PortfolioContext context)
            : base(context)
        {
            //Add(new TagType { Name = "Category" });
            //Add(new TagType { Name = "Framework" });
            //Add(new TagType { Name = "Keyword" });
            //Add(new TagType { Name = "Programming Language" });
            //Add(new TagType { Name = "Other" });
        }
    }
}
