using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Tinyblog.Contracts.Data;
using Tinyblog.Contracts.Services;

namespace Tinyblog.Proxies.Shared
{
    /// <summary>
    /// Implements ITinyblogService.
    /// </summary>
    /// <seealso cref="System.ServiceModel.ClientBase{Tinyblog.Contracts.Services.ITinyblogService}"/>
    /// <seealso cref="Tinyblog.Contracts.Services.ITinyblogService"/>
    public class TinyblogClient : ClientBase<ITinyblogService>, ITinyblogService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TinyblogClient"/> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        public TinyblogClient(string endpoint)
            : base(endpoint)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TinyblogClient"/> class.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <param name="address">The address.</param>
        public TinyblogClient(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }

        /// <summary>
        /// Adds the article information.
        /// </summary>
        /// <param name="articleInfo">The article information.</param>
        /// <returns>
        /// Article Info.
        /// </returns>
        public void AddArticle(ArticleInfo articleInfo)
        {
            Channel.AddArticle(articleInfo);
        }

        public void AddComment(CommentInfo commentInfo)
        {
            Channel.AddComment(commentInfo);
        }

        /// <summary>
        /// Deletes the article.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteArticle(Guid id)
        {
            Channel.DeleteArticle(id);
        }

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteComment(Guid id)
        {
            Channel.DeleteComment(id);
        }

        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Article.</returns>
        public ArticleInfo GetArticleInfo(Guid id)
        {
            return Channel.GetArticleInfo(id);
        }

        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns>Collection of items.</returns>
        public List<ArticlePreviewInfo> GetArticlePreviews()
        {
            return Channel.GetArticlePreviews();
        }
    }
}
