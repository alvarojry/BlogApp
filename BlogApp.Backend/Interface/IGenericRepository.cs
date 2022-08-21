using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Backend.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(long id);

        IQueryable<T> GetAll();

        bool Insert(T entity);

        bool Update(T entity);

        bool Delete(long id);

        bool Delete(T entity);
    }
}
