using System;
using System.Collections.Generic;
using Tinyblog.Common.Log;
using Tinyblog.Contracts.Data;
using Tinyblog.Contracts.Services;

namespace Tinyblog.Client.Services.Implementations
{
    /// <summary>
    /// Service for work with articles.
    /// </summary>
    /// <seealso cref="Tinyblog.Client.Services.IArticleService"/>
    public class ArticleService : IArticleService
    {
        private readonly ILogger logger;
        private readonly ITinyblogService proxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleService"/> class.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        /// <param name="logger">Logger.</param>
        public ArticleService(ITinyblogService proxy, ILogger logger)
        {
            this.proxy = proxy;
            this.logger = logger;
        }

        /// <summary>
        /// Adds the article information.
        /// </summary>
        /// <param name="articleInfo"></param>
        /// <returns>
        /// Added Article.
        /// </returns>
        public void AddArticle(ArticleInfo articleInfo)
        {
            try
            {
                logger.Debug("Try to send request to add article.");
                proxy.AddArticle(articleInfo);
            }
            catch (Exception e)
            {
                logger.Error(e, "Error while sending request to add article.");
                throw;
            }
        }

        public void AddComment(CommentInfo commentInfo)
        {
            try
            {
                logger.Debug("Try to send request to add comment.");
                proxy.AddComment(commentInfo);
            }
            catch (Exception e)
            {
                logger.Error(e, "Error while sending request to add comment.");
                throw;
            }
        }

        /// <summary>
        /// Deletes the article.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteArticle(Guid id)
        {
            try
            {
                logger.Debug($"Try to delete article, id={id}.");
                proxy.DeleteArticle(id);
            }
            catch (Exception e)
            {
                logger.Error(e, $"Error while delete article from server, article id={id}.");
                throw;
            }
        }

        public void DeleteComment(Guid id)
        {
            try
            {
                logger.Debug($"Try to delete comment, id={id}.");
                proxy.DeleteComment(id);
            }
            catch (Exception e)
            {
                logger.Error(e, $"Error while delete comment from server, comment id={id}.");
                throw;
            }
        }

        /// <summary>
        /// Gets the article information.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Article.
        /// </returns>
        public ArticleInfo GetArticleInfo(Guid id)
        {
            try
            {
                logger.Debug($"Try to get article, id={id}.");
                return proxy.GetArticleInfo(id);
            }
            catch (Exception e)
            {
                logger.Error(e, $"Error while get article from server, article id={id}.");
                throw;
            }
        }

        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns>Collection of items.</returns>
        public List<ArticlePreviewInfo> GetArticlePreviews()
        {
            try
            {
                logger.Debug("Try to get articles previews.");
                return proxy.GetArticlePreviews();
            }
            catch (Exception e)
            {
                logger.Error(e, "Error while get article previews from server.");
                throw;
            }
        }
    }
}
