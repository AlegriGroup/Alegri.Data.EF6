using System;
using System.Linq;

namespace Alegri.Data.EF6
{
    /// <summary>
    /// Base implementation of a tracked entity
    /// </summary>
    /// <typeparam name="TEntity">Tracked entity</typeparam>
    public abstract class TrackedRepository<TEntity> : ValidatableRepository<TEntity>, ITrackedRepository<TEntity> where TEntity : ValidatableEntity, IEntity, ITrackedEntity
    {
        /// <summary>
        /// Creates an instance with the given db context
        /// </summary>
        protected TrackedRepository(IDatabaseContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Adds an entity to the context and sets the creator name
        /// </summary>
        public TEntity Add(TEntity entity, string addedBy)
        {
            return base.Add(entity.SetCreated(addedBy));
        }

        /// <summary>
        /// Updates an entity to the context and sets the updater name
        /// </summary>
        public TEntity Update(TEntity entity, string updatedBy)
        {
            return base.Update(entity.SetUpdated(updatedBy));
        }

        /// <summary>
        /// Returns an entity by given id
        /// </summary>
        /// <param name="id">id to search for</param>
        /// <returns>null if not found, and null if found but not deleted</returns>
        public TEntity GetDeleted(Guid id)
        {
            TEntity entity = base.Get(id);
            if(entity == null)
            {
                return null;
            }

            return entity.IsDeleted() ? entity : null;
        }

        /// <summary>
        /// Returns an entity by given id
        /// </summary>
        /// <param name="id">id to search for</param>
        /// <returns>null if not found, and null if found but deleted</returns>
        public TEntity GetNotDeleted(Guid id)
        {
            TEntity entity = base.Get(id);
            if (entity == null)
            {
                return null;
            }

            return entity.IsDeleted() ? null : entity;
        }

        /// <summary>
        /// Returns all not deleted entities with optional <paramref name="clause"/>
        /// </summary>
        public IQueryable<TEntity> GetAllNotDeleted(Func<TEntity, bool> clause = null)
        {
            var query = base.GetMany(entity => entity.DeletedOn == null);

            if(clause != null)
            {
                query = query.Where(clause).AsQueryable();
            }

            return query;
        }

        /// <summary>
        /// Returns all deleted entities with optional <paramref name="clause"/>
        /// </summary>
        public IQueryable<TEntity> GetAllDeleted(Func<TEntity, bool> clause = null)
        {
            var query = base.GetMany(entity => entity.DeletedOn != null);

            if(clause != null)
            {
                query = query.Where(clause).AsQueryable();
            }

            return query;
        }

        /// <summary>
        /// marks an entity as deleted. does not remove entity
        /// </summary>
        public TEntity Delete(TEntity entity, string deletedBy, string reason)
        {
            entity = entity.SetDeleted(deletedBy, reason);

            return base.Update(entity);
            // No delete here! Just mark as deleted
        }

        /// <summary>
        /// marks an entity as deleted. does not remove entity
        /// </summary>
        public TEntity Undelete(TEntity entity, string undeletedBy)
        {
            entity = entity.SetUndeleted(undeletedBy);

            return base.Update(entity);
            // Just mark as undeleted
        }
    }
}