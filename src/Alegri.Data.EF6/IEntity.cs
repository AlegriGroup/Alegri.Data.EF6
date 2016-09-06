using System;

namespace Alegri.Data.EF6
{
    /// <summary>
    /// Entity contract
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Unique Identifier
        /// </summary>
        Guid Id { get; }
    }
}
