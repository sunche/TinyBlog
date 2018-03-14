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
        public Guid ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }
    }
}
