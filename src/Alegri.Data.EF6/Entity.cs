using System;
using System.ComponentModel.DataAnnotations;

namespace Alegri.Data.EF6
{
    public abstract class Entity : IEntity
    {
        [Required]
        public Guid Id { get; set; }
    }
}