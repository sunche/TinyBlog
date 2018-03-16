using System;

namespace Tinyblog.DataLayer.Model
{
    /// <summary>
    /// Comment of the article.
    /// </summary>
    public class Comment : BaseEntity
    {
        /// <summary>
        /// Gets or sets the article identifier.
        /// </summary>
        public virtual Guid ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public virtual string Author { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public virtual string Text { get; set; }
    }
}
