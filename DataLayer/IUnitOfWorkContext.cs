using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IUnitOfWorkContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveAllChanges();
        Task<int> SaveChangesAsync();
    }
}
