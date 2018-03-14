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
        /// Deletes the article.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteArticle(Guid id);

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteComment(Guid id);

        /// <summary>
        /// Gets the article information.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Article.</returns>
        ArticleInfo GetArticleInfo(Guid id);

        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns>Collection of items.</returns>
        List<ArticlePreviewInfo> GetArticlePreviews();
    }
}
