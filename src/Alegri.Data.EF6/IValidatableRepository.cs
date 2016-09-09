namespace Alegri.Data.EF6
{
    /// <summary>
    /// Base Repository for validatable entities
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IValidatableRepository<TEntity> : IBaseRepository<TEntity> where TEntity : ValidatableEntity
    {

    }
}