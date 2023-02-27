using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<AuthCode> AuthCodes { get; set; }

        public ApplicationContext()
        {

            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public ApplicationContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
