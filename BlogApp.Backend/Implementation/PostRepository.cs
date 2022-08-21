using BlogApp.Backend.Entities;
using BlogApp.Backend.Interface;
using BlogApp.Common.Model.Blog;

namespace BlogApp.Backend.Implementation
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
