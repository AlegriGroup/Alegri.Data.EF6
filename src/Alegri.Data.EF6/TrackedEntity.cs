using System;
using System.ComponentModel.DataAnnotations;

namespace Alegri.Data.EF6
{
    public abstract class TrackedEntity : Entity, ITrackedEntity
    {
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime LastUpdatedOn { get; set; }
        [Required]
        public string LastUpdatedBy { get; set; }

        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public string DeletedReason { get; set; }
    }
}