using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alegri.Data.EF6
{
    public class InvalidEntityException<TEntity> : Exception
    {
        public IEnumerable<ValidationResult> Errors { get; }
        public TEntity Entity { get; }

        public InvalidEntityException(IEnumerable<ValidationResult> errors, TEntity entity)
        {
            Errors = errors;
            Entity = entity;
        }
    }
}
