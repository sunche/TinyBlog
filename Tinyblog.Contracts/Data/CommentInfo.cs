﻿using System;
using System.Runtime.Serialization;

namespace Tinyblog.Contracts.Data
{
    /// <summary>
    /// Comment info.
    /// </summary>
    [DataContract]
    public class CommentInfo
    {
        /// <summary>
        /// Gets or sets the article identifier.
        /// </summary>
        [DataMember]
        public Guid ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [DataMember]
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [DataMember]
        public string Text { get; set; }
    }
}
