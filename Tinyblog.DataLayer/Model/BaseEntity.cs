using System;
using System.ComponentModel.DataAnnotations;

namespace Tinyblog.DataLayer.Model
{
    /// <summary>
    /// Parent class for models.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        public virtual Guid Id { get; set; }
    }
}
