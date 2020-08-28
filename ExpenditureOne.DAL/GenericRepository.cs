using System;
using System.Collections.Generic;

namespace ExpenditureOne.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        void CreateBulk(IEnumerable<T> items);
        void Create(T item);
        T FindById(int id);
        T FindById(string id);
        void Remove(T item);
        void Update(T item);
        void Dispose();
    }
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private readonly ExpenditureContext _context;
        public GenericRepository(ExpenditureContext context)
        {
            _context = context;
        }

        public void Create(T item)
        {
            _context.Add(item);
            _context.SaveChanges();
        }

        public void CreateBulk(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public T FindById(int id)
        {
            throw new NotImplementedException();
        }

        public T FindById(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
