using System;

namespace Alegri.Data.EF6
{
    /// <summary>
    /// Extension class for entities
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Marks entity as created
        /// </summary>
        /// <typeparam name="TEntity">Affected entity as extension method</typeparam>
        /// <param name="entity">Entity to mark</param>
        /// <param name="createdBy">Text of created by</param>
        /// <param name="createdOn">Time of create. If no time is passed, <see cref="DateTime.UtcNow"/> is used.</param>
        /// <returns>The entity with the updated fields</returns>
        public static TEntity SetCreated<TEntity>(this TEntity entity, string createdBy, DateTime? createdOn = null) where TEntity : class, ITrackedEntity
        {
            entity.CreatedOn = entity.LastUpdatedOn = createdOn ?? DateTime.UtcNow;
            entity.CreatedBy = entity.LastUpdatedBy = createdBy;

            return entity;
        }

        /// <summary>
        /// Marks entity as updated
        /// </summary>
        /// <typeparam name="TEntity">Affected entity as extension method</typeparam>
        /// <param name="entity">Entity to mark</param>
        /// <param name="updatedBy">Text of updated by</param>
        /// <param name="updatedOn">Time of update. If no time is passed, <see cref="DateTime.UtcNow"/> is used.</param>
        /// <returns>The entity with the updated fields</returns>
        public static TEntity SetUpdated<TEntity>(this TEntity entity, string updatedBy, DateTime? updatedOn = null) where TEntity : class, ITrackedEntity
        {
            entity.LastUpdatedOn = updatedOn ?? DateTime.UtcNow;
            entity.LastUpdatedBy = updatedBy;

            return entity;
        }


        /// <summary>
        /// Marks entity as deleted
        /// </summary>
        /// <typeparam name="TEntity">Affected entity as extension method</typeparam>
        /// <param name="entity">Entity to mark</param>
        /// <param name="deletedBy">Text of deleted by</param>
        /// <param name="reason">Reason of delete</param>
        /// <param name="deletedOn">Time of delete. If no time is passed, <see cref="DateTime.UtcNow"/> is used.</param>
        /// <returns>The entity with the updated fields</returns>
        public static TEntity SetDeleted<TEntity>(this TEntity entity, string deletedBy, string reason, DateTime? deletedOn = null) where TEntity : class, ITrackedEntity
        {
            var time = deletedOn ?? DateTime.UtcNow;

            entity.LastUpdatedOn = time;
            entity.DeletedOn = time;

            entity.LastUpdatedBy = deletedBy;
            entity.DeletedBy = deletedBy;

            entity.DeletedReason = reason;

            return entity;
        }

        /// <summary>
        /// Marks entity as undeleted
        /// </summary>
        /// <typeparam name="TEntity">Affected entity as extension method</typeparam>
        /// <param name="entity">Entity to mark</param>
        /// <param name="undeletedBy">Text of undeleted by</param>
        /// <returns>The entity with the updated fields</returns>
        public static TEntity SetUndeleted<TEntity>(this TEntity entity, string undeletedBy) where TEntity : class, ITrackedEntity
        {
            var time = DateTime.UtcNow;

            entity.LastUpdatedOn = time;
            entity.DeletedOn = null;

            entity.LastUpdatedBy = undeletedBy;
            entity.DeletedBy = null;

            entity.DeletedReason = null;

            return entity;
        }
    }
}
