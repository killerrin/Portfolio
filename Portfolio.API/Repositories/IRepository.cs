using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Repositories
{
    public interface IRepository<T> where T : class
    {
        (PortfolioContext Context, DbSet<T> DbSet) DatabaseInfo { get; }
        int Count { get; }

        void Add(T item);
        bool Exists(int key);
        T Find(int key);
        IEnumerable<T> GetAll();
        IQueryable<T> GetAllQuery();
        void Remove(int key);
        void Update(T item);

        void Commit();
        void Dispose();
    }
}
