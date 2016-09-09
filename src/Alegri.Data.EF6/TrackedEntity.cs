using System;
using System.ComponentModel.DataAnnotations;

namespace Alegri.Data.EF6
{
    /// <summary>
    /// Implementation of an tracked entity
    /// </summary>
    public abstract class TrackedEntity : Entity, ITrackedEntity
    {
        /// <summary>
        /// DateTime of created on. Should be Utc.
        /// </summary>
        [Required]
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Name of the creator
        /// </summary>
        [Required]
        public string CreatedBy { get; set; }

        /// <summary>
        /// DateTime of updated on. Should be Utc.
        /// </summary>
        [Required]
        public DateTime LastUpdatedOn { get; set; }
        /// <summary>
        /// Name of the last changer
        /// </summary>
        [Required]
        public string LastUpdatedBy { get; set; }

        /// <summary>
        /// DateTime of deleted on. Should be Utc.
        /// </summary>
        public DateTime? DeletedOn { get; set; }
        /// <summary>
        /// Name of the last deleter
        /// </summary>
        public string DeletedBy { get; set; }
        /// <summary>
        /// Delete reason
        /// </summary>
        public string DeletedReason { get; set; }

        /// <summary>
        /// An entity is deleted if <see cref="DeletedOn"/> is not null.
        /// </summary>
        /// <returns>true if <see cref="DeletedOn"/> is set</returns>
        public bool IsDeleted()
        {
            return DeletedOn != null;
        }
    }
}