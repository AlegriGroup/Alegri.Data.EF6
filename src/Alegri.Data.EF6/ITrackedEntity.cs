using System;

namespace Alegri.Data.EF6
{
    public interface ITrackedEntity : IEntity
    {
        DateTime CreatedOn { get; set; }
        string CreatedBy { get; set; }

        DateTime LastUpdatedOn { get; set; }
        string LastUpdatedBy { get; set; }

        DateTime? DeletedOn { get; set; }
        string DeletedBy { get; set; }
        string DeletedReason { get; set; }
    }
}