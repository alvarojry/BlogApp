using BlogApp.Backend.Entities;
using BlogApp.Common.Model.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;

namespace BlogApp.Backend.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var dbContext = services.ServiceProvider.GetService<ApplicationContext>();
            dbContext.Database.Migrate();
        }

        public static void CreateBasicUsersAsync(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var dbContext = services.ServiceProvider.GetService<ApplicationContext>();
            if (!dbContext.Users.Any(u => u.Username == "ocabrera")) _ = dbContext.Users.Add(CreatePublic());
            if (!dbContext.Users.Any(u => u.Username == "afuente")) _ = dbContext.Users.Add(CreateEditor());
            if (!dbContext.Users.Any(u => u.Username == "psanchez")) _ = dbContext.Users.Add(CreateWriter());
            if (!dbContext.Users.Any(u => u.Username == "landrade")) _ = dbContext.Users.Add(CreateWriter2());
            dbContext.SaveChanges();
        }

        private static User CreatePublic()
        {
            return new User
            {
                Name = "Orlando Cabrera",
                Password = Convert.ToBase64String(Encoding.ASCII.GetBytes("password")),
                Username = "ocabrera",
                Role = Role.PUBLIC
            };
        }

        private static User CreateEditor()
        {
            return new User
            {
                Name = "Andres Fuente",
                Password = Convert.ToBase64String(Encoding.ASCII.GetBytes("password")),
                Username = "afuente",
                Role = Role.EDITOR
            };
        }

        private static User CreateWriter()
        {
            return new User
            {
                Name = "Paula Sanchez",
                Password = Convert.ToBase64String(Encoding.ASCII.GetBytes("password")),
                Username = "psanchez",
                Role = Role.WRITER
            };
        }

        private static User CreateWriter2()
        {
            return new User
            {
                Name = "Laura Andrade",
                Password = Convert.ToBase64String(Encoding.ASCII.GetBytes("password")),
                Username = "landrade",
                Role = Role.WRITER
            };
        }
    }
}
