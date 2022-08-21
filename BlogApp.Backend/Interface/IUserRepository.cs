using BlogApp.Common.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Backend.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetByUsername(string username);
    }
}
