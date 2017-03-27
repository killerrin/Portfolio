using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Models
{
    public class ProgrammingLanguageRepository : IRepository<ProgrammingLanguage>
    {
        private readonly PortfolioContext _context;

        public ProgrammingLanguageRepository(PortfolioContext context)
        {
            _context = context;
            Add(new ProgrammingLanguage { Name = "C#" });
        }

        public void Add(ProgrammingLanguage item)
        {
            _context.ProgrammingLanguages.Add(item);
            _context.SaveChanges();
        }

        public IEnumerable<ProgrammingLanguage> GetAll()
        {
            return _context.ProgrammingLanguages.ToList();
        }

        public ProgrammingLanguage Find(int key)
        {
            return _context.ProgrammingLanguages.FirstOrDefault(x => x.Key == key);
        }

        public void Remove(int key)
        {
            var entity = _context.ProgrammingLanguages.First(x => x.Key == key);
            _context.ProgrammingLanguages.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(ProgrammingLanguage item)
        {
            _context.ProgrammingLanguages.Update(item);
            _context.SaveChanges();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
