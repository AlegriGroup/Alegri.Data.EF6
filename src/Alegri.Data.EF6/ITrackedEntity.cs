using System;

namespace Alegri.Data.EF6
{
    /// <summary>
    /// Contract of an tracked entity
    /// </summary>
    public interface ITrackedEntity : IEntity
    {
        /// <summary>
        /// DateTime of created on. Should be Utc.
        /// </summary>
        DateTime CreatedOn { get; set; }
        /// <summary>
        /// Name of the creator
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// DateTime of updated on. Should be Utc.
        /// </summary>
        DateTime LastUpdatedOn { get; set; }

        /// <summary>
        /// Name of the last changer
        /// </summary>
        string LastUpdatedBy { get; set; }

        /// <summary>
        /// DateTime of deleted on. Should be Utc.
        /// </summary>
        DateTime? DeletedOn { get; set; }
        /// <summary>
        /// Name of the last deleter
        /// </summary>
        string DeletedBy { get; set; }

        /// <summary>
        /// Delete reason
        /// </summary>
        string DeletedReason { get; set; }

        /// <summary>
        /// An entity is deleted if <see cref="DeletedOn"/> is not null.
        /// </summary>
        /// <returns>true if <see cref="DeletedOn"/> is set</returns>
        bool IsDeleted();
    }
}