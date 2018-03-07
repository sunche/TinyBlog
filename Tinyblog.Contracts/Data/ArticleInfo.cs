﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Tinyblog.Contracts.Data
{
    /// <summary>
    /// DTO with article properties.
    /// </summary>
    [DataContract]
    public class ArticleInfo
    {
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        [DataMember]
        public IList<CommentInfo> Comments { get; set; }

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
