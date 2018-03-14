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
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the text of the article.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the title of the article.
        /// </summary>
        public string Title { get; set; }
    }
}
