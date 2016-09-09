using System;
using System.Data.Entity;
using System.Linq;

namespace Alegri.Data.EF6
{
    public interface ITrackedRepository<TEntity> where TEntity : ValidatableEntity, IEntity, ITrackedEntity
    {
        /// <summary>
        /// Adds an entity to the context and sets the creator name
        /// </summary>
        TEntity Add(TEntity entity, string addedBy);

        /// <summary>
        /// Updates an entity to the context and sets the updater name
        /// </summary>
        TEntity Update(TEntity entity, string updatedBy);

        /// <summary>
        /// Returns an entity by given if not deleted
        /// </summary>
        /// <param name="id">id to search for</param>
        /// <returns>null if not found or deleted</returns>
        TEntity Get(Guid id);

        /// <summary>
        /// Returns an entity by given if deleted
        /// </summary>
        /// <param name="id">id to search for</param>
        /// <returns>null if not found or not deleted</returns>
        TEntity GetDeleted(Guid id);

        /// <summary>
        /// Returns all not deleted entities with optional <paramref name="clause"/>
        /// </summary>
        IQueryable<TEntity> GetAllNotDeleted(Func<TEntity, bool> clause = null);

        /// <summary>
        /// Returns all deleted entities with optional <paramref name="clause"/>
        /// </summary>
        IQueryable<TEntity> GetAllDeleted(Func<TEntity, bool> clause = null);

        /// <summary>
        /// Returns all not deleted entities
        /// </summary>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Returns <see cref="TrackedRepository{TEntity}.GetAllNotDeleted"/> with clause
        /// </summary>
        IQueryable<TEntity> GetMany(Func<TEntity, bool> clause);

        /// <summary>
        /// marks an entity as deleted. does not remove entity
        /// </summary>
        TEntity Delete(TEntity entity, string deletedBy, string reason);

        /// <summary>
        /// Current DbContext
        /// </summary>
        IDatabaseContext DbContext { get; }

        /// <summary>
        /// Current Entity Set
        /// </summary>
        DbSet<TEntity> CurrentSet { get; }

        /// <summary>
        /// Returns a single entity with the matchin clause
        /// </summary>
        /// <param name="clause">clause for filter on an entity</param>
        /// <returns>null if no item matches</returns>
        TEntity Get(Func<TEntity, bool> clause);

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