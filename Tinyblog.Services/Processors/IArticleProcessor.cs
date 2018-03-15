using System;
using System.Collections.Generic;
using Tinyblog.Contracts.Data;

namespace Tinyblog.Services.Processors
{
    /// <summary>
    /// Interface to work with articles.
    /// </summary>
    public interface IArticleProcessor
    {
        /// <summary>
        /// Adds the article.
        /// </summary>
        /// <param name="articleInfo">The article information.</param>
        /// <returns>Added article.</returns>
        void AddArticle(ArticleInfo articleInfo);

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="commentInfo">The comment information.</param>
        /// <returns>Comment info.</returns>
        void AddComment(CommentInfo commentInfo);

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
        /// <returns>Article info.</returns>
        ArticleInfo GetArticleInfo(Guid id);

        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns>Article preview collection.</returns>
        List<ArticlePreviewInfo> GetArticlePreviews();
    }
}
