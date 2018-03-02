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
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public string UserName { get; set; }
    }
}
