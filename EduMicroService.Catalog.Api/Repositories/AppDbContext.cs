using EduMicroService.Catalog.Api.Features.Categories;
using EduMicroService.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Reflection;

namespace EduMicroService.Catalog.Api.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }

        public static AppDbContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<AppDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

    }
}
