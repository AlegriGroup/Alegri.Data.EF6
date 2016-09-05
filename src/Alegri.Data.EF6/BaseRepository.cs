using System;
using System.Data.Entity;
using System.Linq;

namespace Alegri.Data.EF6
{
    public abstract class BaseRepository<TEntity>
        where TEntity : class, IEntity
    {
        public StringComparison ComparisonCulture = StringComparison.InvariantCultureIgnoreCase;

        public IDatabaseContext DbContext { get; }
        public DbSet<TEntity> CurrentSet { get; }

        protected BaseRepository(IDatabaseContext dbContext)
        {
            DbContext = dbContext;
            CurrentSet = DbContext.Set<TEntity>();
        }

        // CRUD
        public virtual TEntity Get(Guid id)
        {
            return Get(entity => entity.Id == id);
        }
        public virtual TEntity Get(Func<TEntity, bool> clause)
        {
            return CurrentSet.SingleOrDefault(clause);
        }
        public virtual IQueryable<TEntity> GetAll()
        {
            return CurrentSet.AsQueryable();
        }
        public virtual IQueryable<TEntity> GetMany(Func<TEntity, bool> clause)
        {
            return CurrentSet.Where(clause).AsQueryable();
        }

        public virtual TEntity Add(TEntity entity)
        {
            return CurrentSet.Add(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            var updated = CurrentSet.Attach(entity);
            DbContext.Entry(updated).State = EntityState.Modified;
            return updated;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            return CurrentSet.Remove(entity);
        }
        public virtual bool Exists(Guid id)
        {
            return CurrentSet.Any(entity => entity.Id == id);
        }

        // Save
        public int Save()
        {
            return DbContext.SaveChanges();
        }
    }
}
