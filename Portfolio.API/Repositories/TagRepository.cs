using Portfolio.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Repositories
{
    public class TagRepository : RepositoryBase<Tag>
    {
        public TagRepository(PortfolioContext context)
            : base(context)
        {
            //Add(new Tag { Name = "C#", TagTypeID = 4 });
            //Add(new Tag { Name = "C++", TagTypeID = 4 });
            //Add(new Tag { Name = "OOP", TagTypeID = 3 });
            //Add(new Tag { Name = "Json", TagTypeID = 3 });
            //Add(new Tag { Name = "XML", TagTypeID = 3 });
        }
    }
}
