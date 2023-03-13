using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ITEPortal.Data.Persistence
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base()
        {
        }

        public ApplicationContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
