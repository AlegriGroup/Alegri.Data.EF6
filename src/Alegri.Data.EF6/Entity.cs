using System;
using System.ComponentModel.DataAnnotations;

namespace Alegri.Data.EF6
{
    /// <summary>
    /// Base entity implementation
    /// </summary>
    public abstract class Entity : IEntity
    {
        /// <summary>
        /// Id of entity
        /// </summary>
        [Required]
        public Guid Id { get; set; }
    }
}