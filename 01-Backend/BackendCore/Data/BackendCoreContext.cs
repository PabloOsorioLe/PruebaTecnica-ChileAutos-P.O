using Microsoft.EntityFrameworkCore;
using BackendCore.DTOs;

namespace BackendCore.Data
{
    public class BackendCoreContext : DbContext
    {
        public BackendCoreContext(DbContextOptions<BackendCoreContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
        }

    }
}
