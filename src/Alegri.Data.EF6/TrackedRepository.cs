using System;
using System.Linq;

namespace Alegri.Data.EF6
{
    public abstract class TrackedRepository<TEntity> : ValidatableRepository<TEntity>
        where TEntity : ValidatableEntity, IEntity, ITrackedEntity
    {
        protected TrackedRepository(IDatabaseContext dbContext) : base(dbContext)
        {
        }

        public TEntity Add(TEntity entity, string addedBy)
        {
            entity = entity.SetCreated(addedBy);

            return base.Add(entity);
        }

        public TEntity Update(TEntity entity, string updatedBy)
        {
            entity = entity.SetUpdated(updatedBy);

            return base.Update(entity);
        }

        public override TEntity Get(Guid id)
        {
            var entity = base.Get(id);

            return entity.DeletedOn == null ? entity : null;
        }

        public IQueryable<TEntity> GetAllNotDeleted(Func<TEntity, bool> clause = null)
        {
            var query = base.GetMany(entity => entity.DeletedOn == null);

            if(clause != null)
            {
                query = query.Where(clause).AsQueryable();
            }

            return query;
        }

        public override IQueryable<TEntity> GetAll()
        {
            return GetAllNotDeleted();
        }
        public override IQueryable<TEntity> GetMany(Func<TEntity, bool> clause)
        {
            return GetAllNotDeleted(clause);
        }

        public TEntity Delete(TEntity entity, string deletedBy)
        {
            entity = entity.SetDeleted(deletedBy);

            return base.Update(entity);
            // No delete here! Just mark as deleted
        }
    }
}