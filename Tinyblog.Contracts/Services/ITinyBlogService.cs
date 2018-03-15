using System;
using System.Collections.Generic;
using System.ServiceModel;
using Tinyblog.Contracts.Data;

namespace Tinyblog.Contracts.Services
{
    /// <summary>
    /// Interface for TinyblogService
    /// </summary>
    [ServiceContract]
    public interface ITinyblogService
    {
        /// <summary>
        /// Adds the article information.
        /// </summary>
        /// <param name="articleInfo">The article information.</param>
        /// <returns>Article Info.</returns>
        [OperationContract]
        void AddArticle(ArticleInfo articleInfo);

        /// <summary>
        /// Adds the comment information.
        /// </summary>
        /// <param name="commentInfo">The comment information.</param>
        /// <returns>Comment info.</returns>
        [OperationContract]
        void AddComment(CommentInfo commentInfo);

        /// <summary>
        /// Deletes the article.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [OperationContract]
        void DeleteArticle(Guid id);

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [OperationContract]
        void DeleteComment(Guid id);

        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns>Article info.</returns>
        [OperationContract]
        ArticleInfo GetArticleInfo(Guid id);

        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns>Article info.</returns>
        [OperationContract]
        List<ArticlePreviewInfo> GetArticlePreviews();
    }
}
