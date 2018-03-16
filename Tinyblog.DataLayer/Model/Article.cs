using System;

namespace Tinyblog.DataLayer.Model
{
    /// <summary>
    /// Article entity
    /// </summary>
    public class Article : BaseEntity
    {
        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public virtual string Author { get; set; }

        /// <summary>
        /// Gets or sets the text of the article.
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// Gets or sets the title of the article.
        /// </summary>
        public virtual string Title { get; set; }
    }
}
