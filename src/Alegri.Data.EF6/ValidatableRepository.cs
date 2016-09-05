using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alegri.Data.EF6
{
    public abstract class ValidatableRepository<TEntity> : BaseRepository<TEntity> where TEntity : ValidatableEntity
    {
        protected ValidatableRepository(IDatabaseContext dbContext) : base(dbContext)
        {
        }

        public new TEntity Add(TEntity entity)
        {
            ThrowIfEntityInvalid(entity);
            return base.Add(entity);
        }

        public new TEntity Update(TEntity entity)
        {
            ThrowIfEntityInvalid(entity);
            return base.Update(entity);
        }

        public new TEntity Delete(TEntity entity)
        {
            ThrowIfEntityInvalid(entity);
            return base.Update(entity);
        }

        private void ThrowIfEntityInvalid(TEntity entity)
        {
            IEnumerable<ValidationResult> errors;
            if(!entity.IsValid(out errors))
            {
                throw new InvalidEntityException<TEntity>(errors, entity);
            }
        }
    }
}