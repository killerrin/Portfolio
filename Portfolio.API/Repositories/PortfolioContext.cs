using Microsoft.EntityFrameworkCore;
using Portfolio.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Repositories
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext(DbContextOptions<PortfolioContext> options)
            : base(options)
        {

        }

        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    }
}
