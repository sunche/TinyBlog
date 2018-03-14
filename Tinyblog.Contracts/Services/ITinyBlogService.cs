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
