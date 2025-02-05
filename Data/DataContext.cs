using BlogApi.Data.Mapping;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data 
{
    public class DataContext : DbContext 
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=localhost,1433;Database=Blog;User ID=SA;Password=1q2w3e4r@#$;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PostMap());
        }
    }
}