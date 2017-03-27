using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Repositories
{
    public interface IRepository<T>
    {
        int Count { get; }

        void Add(T item);
        bool Exists(int key);
        T Find(int key);
        IEnumerable<T> GetAll();
        void Remove(int key);
        void Update(T item);

        void Commit();
        void Dispose();
    }
}
