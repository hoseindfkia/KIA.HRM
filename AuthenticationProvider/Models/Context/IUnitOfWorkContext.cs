using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AuthenticationProvider.Models.Context
{
    public interface IUnitOfWorkContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveAllChanges();
        Task<int> SaveChangesAsync();
    }
}
