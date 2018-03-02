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
        public Guid Id { get; set; }
    }
}
