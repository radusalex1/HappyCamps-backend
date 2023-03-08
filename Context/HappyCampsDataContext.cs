using HappyCamps_backend.Common;
using HappyCamps_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HappyCamps_backend.Context
{
    public class HappyCampsDataContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public HappyCampsDataContext(DbContextOptions<HappyCampsDataContext>options):
            base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns()
                        .Entity<User>()
                        .Property(x => x.RoleType)
                        .HasConversion(new EnumToStringConverter<Role>());
        }
    }
}
