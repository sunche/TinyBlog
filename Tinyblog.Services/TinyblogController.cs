using System;
using System.Collections.Generic;
using System.ServiceModel;
using Tinyblog.Common.Log;
using Tinyblog.Contracts.Data;
using Tinyblog.Contracts.Services;
using Tinyblog.Services.Processors;

namespace Tinyblog.Services
{
    /// <summary>
    /// Implement ITinyblogService.
    /// </summary>
    /// <seealso cref="ITinyblogService"/>
    public class TinyblogController : ITinyblogService
    {
        private readonly IArticleProcessor articleProcessor;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TinyblogController"/> class.
        /// </summary>
        /// <param name="articleProcessor">The article processor.</param>
        /// <param name="logger">Logger.</param>
        public TinyblogController(IArticleProcessor articleProcessor, ILogger logger)
        {
            this.articleProcessor = articleProcessor;
            this.logger = logger;
        }

        /// <summary>
        /// Deletes the article.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteArticle(Guid id)
        {
            try
            {
                logger.Debug($"Try to delete article with id={id}.");
                articleProcessor.DeleteArticle(id);
            }
            catch (Exception e)
            {
                logger.Error(e, $"Error while deleting article, id=${id}.");
                throw new FaultException(e.Message);
            }
        }

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteComment(Guid id)
        {
            try
            {
                logger.Debug($"Try to delete comment with id={id}.");
                articleProcessor.DeleteComment(id);
            }
            catch (Exception e)
            {
                logger.Error(e, $"Error while deleting comment, id=${id}.");
                throw new FaultException(e.Message);
            }
        }

        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Article.</returns>
        public ArticleInfo GetArticleInfo(Guid id)
        {
            try
            {
                logger.Debug($"GetArticleInfo method is called  with id={id}.");
                return articleProcessor.GetArticleInfo(id);
            }
            catch (Exception e)
            {
                logger.Error(e, $"Error while getting article info for article, id=${id}.");
                throw new FaultException(e.Message);
            }
        }

        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns>ArticlePreviewInfo collection.</returns>
        public List<ArticlePreviewInfo> GetArticlePreviews()
        {
            try
            {
                logger.Debug("GetArticlePreviews method is called");
                return articleProcessor.GetArticlePreviews();
            }
            catch (Exception e)
            {
                logger.Error(e, "Error while getting article previews.");
                throw new FaultException(e.Message);
            }
        }
    }
}
