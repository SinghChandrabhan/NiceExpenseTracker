using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.DataAccess
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        #region Private Member
        private DbContext Context { get; set; }
        private DbSet<TEntity> _dbSet;
        #endregion
        public GenericRepository(DbContext context)
        {
            Context = context;
        }
        protected DbSet<TEntity> DbSet
        {
            get
            {
                if (_dbSet == null)
                    _dbSet = Context.Set<TEntity>();
                return _dbSet;
            }
        }

        #region Db Operations
        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await this.DbSet.ToListAsync();
        }

        public virtual async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await this.DbSet.Where(match).ToListAsync();
        }
        public virtual async Task<object> InsertAsync(TEntity entity)
        {
            var rtn = await this.DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
            return rtn;
        }
        #endregion
    }
}
