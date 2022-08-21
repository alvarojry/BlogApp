using BlogApp.Backend.Entities;
using BlogApp.Backend.Interface;
using BlogApp.Common.Model.Security;
using System.Linq;

namespace BlogApp.Backend.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(user => user.Username == username);
        }
    }
}
