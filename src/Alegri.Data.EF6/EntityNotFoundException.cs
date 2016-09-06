using System;

namespace Alegri.Data.EF6
{
    /// <summary>
    /// Exception if entity was not found
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityNotFoundException<T> : Exception
    {
        /// <summary>
        /// Id to search for
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">Id to search for</param>
        public EntityNotFoundException(T id)
        {
            Id = id;
        }
    }
}