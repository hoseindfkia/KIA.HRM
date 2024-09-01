using AuthenticationProvider.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationProvider.Models.Context
{
    public class UnitOfWorkContext : IdentityDbContext<UserEntity>, IUnitOfWorkContext
    {
        public UnitOfWorkContext(DbContextOptions<UnitOfWorkContext> options)
           : base(options) { }

        public int SaveAllChanges()
        {
            return base.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Table names
            // modelBuilder.Entity<EmployeeEntity>().ToTable("Employee");
            #endregion
        }
    }
}
