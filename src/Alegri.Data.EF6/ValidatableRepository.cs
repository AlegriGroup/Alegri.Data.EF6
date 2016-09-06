using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alegri.Data.EF6
{
    /// <summary>
    /// base implementation for a repository of a validatable entity
    /// </summary>
    /// <typeparam name="TEntity">Validatable entity</typeparam>
    public abstract class ValidatableRepository<TEntity> : BaseRepository<TEntity> where TEntity : ValidatableEntity
    {
        /// <summary>
        /// Creates an instance with given <paramref name="dbContext"/>
        /// </summary>
        protected ValidatableRepository(IDatabaseContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Validates an entity and adds to dbcontext if valid
        /// </summary>
        /// <returns>the added enttiy</returns>
        /// <exception cref="InvalidEntityException{TEntity}"> if invalid</exception>
        public new TEntity Add(TEntity entity)
        {
            ThrowIfEntityInvalid(entity);
            return base.Add(entity);
        }

        /// <summary>
        /// Validates an entity and updates to dbcontext if valid
        /// </summary>
        /// <returns>the updated enttiy</returns>
        /// <exception cref="InvalidEntityException{TEntity}"> if invalid</exception>
        public new TEntity Update(TEntity entity)
        {
            ThrowIfEntityInvalid(entity);
            return base.Update(entity);
        }

        /// <summary>
        /// Validates given <paramref name="entity"/> and throws an <see cref="InvalidEntityException{TEntity}"/> if not valid
        /// </summary>
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