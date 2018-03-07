using System;
using System.Collections.Generic;
using Tinyblog.Contracts.Data;

namespace Tinyblog.Client.Services
{
    /// <summary>
    /// Service for work with articles.
    /// </summary>
    public interface IArticleService
    {
        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns>Collection of items.</returns>
        List<ArticlePreviewInfo> GetArticlePreviews();
    }
}
