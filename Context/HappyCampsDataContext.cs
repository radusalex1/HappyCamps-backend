using HappyCamps_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace HappyCamps_backend.Context
{
    public class HappyCampsDataContext:DbContext
    {
        public HappyCampsDataContext(DbContextOptions<HappyCampsDataContext>options):
            base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<User> Users { get; set; }
    }
}
