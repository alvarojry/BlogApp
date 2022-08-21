using BlogApp.Backend.Entities;
using BlogApp.Backend.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BlogApp.Backend.Implementation
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Delete(T entity)
        {
            _ = _context.Set<T>().Remove(entity);
            return  _context.SaveChanges() == 1;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T Get(long id)
        {
            return _context.Set<T>().Find(id);
        }

        public bool Insert(T entity)
        {
            _ = _context.Set<T>().AddAsync(entity);
            return _context.SaveChanges() == 1;
        }

        public bool Update(T entity)
        {
            _ = _context.Set<T>().Update(entity);
            return _context.SaveChanges() == 1;
        }

        public bool Delete(long id)
        {
            T entity = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges() == 1;
        }
    }
}
