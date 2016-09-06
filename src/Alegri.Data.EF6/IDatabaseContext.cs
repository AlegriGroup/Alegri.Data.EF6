using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Alegri.Data.EF6
{
    /// <summary>
    /// Database context contract
    /// </summary>
    public interface IDatabaseContext
    {
        /// <summary>
        /// Returns a set of the given entity
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <returns>Set of <seeref cref="TEntity"/></returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Commits changes to the context.
        /// </summary>
        /// <returns>Number of changes</returns>
        int SaveChanges();

        /// <summary>
        /// Selects a single entity of current context
        /// </summary>
        /// <typeparam name="TEntity">Entity</typeparam>
        /// <param name="entity"></param>
        /// <returns>The Db entry of given entity</returns>
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
