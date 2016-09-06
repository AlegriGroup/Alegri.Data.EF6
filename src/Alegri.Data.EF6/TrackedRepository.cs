using System;
using System.Linq;

namespace Alegri.Data.EF6
{
    /// <summary>
    /// Base implementation of a tracked entity
    /// </summary>
    /// <typeparam name="TEntity">Tracked entity</typeparam>
    public abstract class TrackedRepository<TEntity> : ValidatableRepository<TEntity>
        where TEntity : ValidatableEntity, IEntity, ITrackedEntity
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
            entity = entity.SetCreated(addedBy);

            return base.Add(entity);
        }

        /// <summary>
        /// Updates an entity to the context and sets the updater name
        /// </summary>
        public TEntity Update(TEntity entity, string updatedBy)
        {
            entity = entity.SetUpdated(updatedBy);

            return base.Update(entity);
        }

        /// <summary>
        /// Returns an entity by given if not deleted
        /// </summary>
        /// <param name="id">id to search for</param>
        /// <returns>null if not found or deleted</returns>
        public override TEntity Get(Guid id)
        {
            var entity = base.Get(id);

            return entity.DeletedOn == null ? entity : null;
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
        /// Returns <see cref="GetAllNotDeleted"/> with no clause
        /// </summary>
        public override IQueryable<TEntity> GetAll()
        {
            return GetAllNotDeleted();
        }

        /// <summary>
        /// Returns <see cref="GetAllNotDeleted"/> with clause
        /// </summary>
        public override IQueryable<TEntity> GetMany(Func<TEntity, bool> clause)
        {
            return GetAllNotDeleted(clause);
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
    }
}