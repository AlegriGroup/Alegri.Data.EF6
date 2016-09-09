using System;
using System.Data.Entity;
using System.Linq;

namespace Alegri.Data.EF6
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Current DbContext
        /// </summary>
        IDatabaseContext DbContext { get; }

        /// <summary>
        /// Current Entity Set
        /// </summary>
        DbSet<TEntity> CurrentSet { get; }

        TEntity Get(Guid id);

        /// <summary>
        /// Returns a single entity with the matchin clause
        /// </summary>
        /// <param name="clause">clause for filter on an entity</param>
        /// <returns>null if no item matches</returns>
        TEntity Get(Func<TEntity, bool> clause);

        /// <summary>
        /// Returns the entity set
        /// </summary>
        /// <returns><see cref="IQueryable{T}"/></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Returns all matching entities
        /// </summary>
        /// <param name="clause">filter</param>
        /// <returns><see cref="IQueryable{TEntity}"/></returns>
        IQueryable<TEntity> GetMany(Func<TEntity, bool> clause);

        /// <summary>
        /// Adds an entity to the current set
        /// </summary>
        /// <param name="entity">entity to add</param>
        /// <returns>returns the added entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Updates the given entity and marks the entity in the current set as modified
        /// </summary>
        /// <param name="entity">entity to update</param>
        /// <returns>returns the updated entity</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Removes the given entity from the current set
        /// </summary>
        /// <param name="entity">entity to remove</param>
        /// <returns>returns the removed entity</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Checks if the given id exists in the current entity set
        /// </summary>
        /// <param name="id">id to search for</param>
        /// <returns>true if id exists</returns>
        bool Exists(Guid id);

        /// <summary>
        /// Commits all chanegs to the current dbContext
        /// </summary>
        /// <returns></returns>
        int Save();
    }
}