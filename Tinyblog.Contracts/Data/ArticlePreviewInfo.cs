using System;
using System.Runtime.Serialization;

namespace Tinyblog.Contracts.Data
{
    /// <summary>
    /// DTO with article preview properties.
    /// </summary>
    [DataContract]
    public class ArticlePreviewInfo
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the article.
        /// </summary>
        [DataMember]
        public string Title { get; set; }
    }
}
