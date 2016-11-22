using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Alegri.Data.EF6
{
    /// <summary>
    /// Include multiple entities with one query adapted from http://stackoverflow.com/questions/5376421/ef-including-other-entities-generic-repository-pattern
    /// </summary>


    public static class IQueryableExtensions
    {
        /// <summary>
        /// Include multiple entities with one query, aka. eager loading
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="includes">the include expressions</param>
        /// <returns></returns>
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
