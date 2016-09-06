using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alegri.Data.EF6
{
    /// <summary>
    /// Exception of an invalid entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class InvalidEntityException<TEntity> : Exception
    {
        /// <summary>
        /// List of validation errors
        /// </summary>
        public IEnumerable<ValidationResult> Errors { get; }

        /// <summary>
        /// Invalid entity
        /// </summary>
        public TEntity Entity { get; }

        /// <summary>
        /// Creates an instance
        /// </summary>
        /// <param name="errors">List of validation errors</param>
        /// <param name="entity">Invalid entity</param>
        public InvalidEntityException(IEnumerable<ValidationResult> errors, TEntity entity)
        {
            Errors = errors;
            Entity = entity;
        }
    }
}
