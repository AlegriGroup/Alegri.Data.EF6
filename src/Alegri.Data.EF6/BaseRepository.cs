using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Alegri.Data.EF6
{
    /// <summary>
    /// This repository offers basic CRUD functions to access a database provider with the Entity Framework
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Default comparison culture on string compares
        /// </summary>
        public StringComparison ComparisonCulture = StringComparison.InvariantCultureIgnoreCase;

        /// <summary>
        /// Current DbContext
        /// </summary>
        public IDatabaseContext DbContext { get; }

        /// <summary>
        /// Current Entity Set
        /// </summary>
        public DbSet<TEntity> CurrentSet { get; }

        /// <summary>
        /// Creates a new instance this base repository
        /// </summary>
        /// <param name="dbContext">Active database context</param>
        protected BaseRepository(IDatabaseContext dbContext)
        {
            DbContext = dbContext;
            CurrentSet = DbContext.Set<TEntity>();
        }

        // CRUD
        /// <summary>
        /// Get the entity by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Get(Guid id)
        {
            return Get(entity => entity.Id == id);
        }

        /// <summary>
        /// Get the entity by its id including related entities
        /// </summary>
        /// <param name="id"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public virtual TEntity Get(Guid id, params Expression<Func<TEntity, object>>[] include)
        {
            return Get(entity => entity.Id == id, include);
        }

        /// <summary>
        /// Returns a single entity with the matchin clause
        /// </summary>
        /// <param name="clause">clause for filter on an entity</param>
        /// <returns>null if no item matches</returns>
        public virtual TEntity Get(Func<TEntity, bool> clause)
        {
            return CurrentSet.SingleOrDefault(clause);
        }

        /// <summary>
        /// Returns a single entity with the matchin clause
        /// </summary>
        /// <param name="clause">clause for filter on an entity</param>
        /// <param name="include">including related entites (eager loading)</param>
        /// <returns>null if no item matches</returns>
        public virtual TEntity Get(Func<TEntity, bool> clause, params Expression<Func<TEntity, object>>[] include)
        {
            return CurrentSet.AsQueryable().IncludeMultiple(include).SingleOrDefault(clause);
        }

        /// <summary>
        /// Returns the entity set
        /// </summary>
        /// <returns><see cref="IQueryable{TEntity}"/></returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            return CurrentSet.AsQueryable();
        }

        /// <summary>
        /// Returns the entity set
        /// </summary>
        /// <returns><see cref="IQueryable{TEntity}"/></returns>
        public virtual IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] include)
        {
            return CurrentSet.IncludeMultiple(include).AsQueryable();
        }

        /// <summary>
        /// Returns all matching entities
        /// </summary>
        /// <param name="clause">filter</param>
        /// <returns><see cref="IQueryable{TEntity}"/></returns>
        public virtual IQueryable<TEntity> GetMany(Func<TEntity, bool> clause)
        {
            return CurrentSet.Where(clause).AsQueryable();
        }

        /// <summary>
        /// Returns all matching entities including related entities
        /// </summary>
        /// <param name="clause">filter</param>
        /// <param name="include">the includes for eager loading</param>
        /// <returns><see cref="IQueryable{TEntity}"/></returns>
        public virtual IQueryable<TEntity> GetMany(Func<TEntity, bool> clause, params Expression<Func<TEntity, object>>[] include)
        {
            return CurrentSet.Where(clause).AsQueryable().IncludeMultiple(include);
        }

        /// <summary>
        /// Adds an entity to the current set
        /// </summary>
        /// <param name="entity">entity to add</param>
        /// <returns>returns the added entity</returns>
        public virtual TEntity Add(TEntity entity)
        {
            return CurrentSet.Add(entity);
        }

        /// <summary>
        /// Updates the given entity and marks the entity in the current set as modified
        /// </summary>
        /// <param name="entity">entity to update</param>
        /// <returns>returns the updated entity</returns>
        public virtual TEntity Update(TEntity entity)
        {
            var updated = CurrentSet.Attach(entity);
            DbContext.Entry(updated).State = EntityState.Modified;
            return updated;
        }

        /// <summary>
        /// Removes the given entity from the current set
        /// </summary>
        /// <param name="entity">entity to remove</param>
        /// <returns>returns the removed entity</returns>
        public virtual TEntity Delete(TEntity entity)
        {
            return CurrentSet.Remove(entity);
        }

        /// <summary>
        /// Checks if the given id exists in the current entity set
        /// </summary>
        /// <param name="id">id to search for</param>
        /// <returns>true if id exists</returns>
        public virtual bool Exists(Guid id)
        {
            return CurrentSet.Any(entity => entity.Id == id);
        }

        /// <summary>
        /// Commits all chanegs to the current dbContext
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            return DbContext.SaveChanges();
        }
    }
}
