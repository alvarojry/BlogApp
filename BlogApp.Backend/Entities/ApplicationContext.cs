using BlogApp.Common.Model.Blog;
using BlogApp.Common.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogApp.Backend.Entities
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ValueConverter roleConverter = new ValueConverter<Role, string>(
                v => v.ToString(),
                v => Role.Parse(v)
            );

            //Declare non nullable columns
            modelBuilder.Entity<User>().HasIndex(idx => idx.Username).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.Role).HasConversion(roleConverter);
        }
    }
}
