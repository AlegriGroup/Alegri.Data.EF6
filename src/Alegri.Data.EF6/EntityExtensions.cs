using System;

namespace Alegri.Data.EF6
{
    public static class EntityExtensions
    {
        public static TEntity SetCreated<TEntity>(this TEntity entity, string createdBy, DateTime? createdOn = null) where TEntity : class, ITrackedEntity
        {
            entity.CreatedOn = entity.LastUpdatedOn = createdOn ?? DateTime.UtcNow;
            entity.CreatedBy = entity.LastUpdatedBy = createdBy;

            return entity;
        }

        public static TEntity SetUpdated<TEntity>(this TEntity entity, string updatedBy, DateTime? updatedOn = null) where TEntity : class, ITrackedEntity
        {
            entity.LastUpdatedOn = updatedOn ?? DateTime.UtcNow;
            entity.LastUpdatedBy = updatedBy;

            return entity;
        }

        public static TEntity SetDeleted<TEntity>(this TEntity entity, string deletedBy, DateTime? deletedOn = null) where TEntity : class, ITrackedEntity
        {
            entity.DeletedOn = deletedOn ?? DateTime.UtcNow;
            entity.DeletedBy = deletedBy;

            return entity;
        }
    }
}
