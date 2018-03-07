using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ITinyblogService proxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleService"/> class.
        /// </summary>
        /// <param name="proxy">The proxy.</param>
        public ArticleService(ITinyblogService proxy)
        {
            this.proxy = proxy;
        }

        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns>Collection of items.</returns>
        public List<ArticlePreviewInfo> GetArticlePreviews()
        {
            return proxy.GetArticlePreviews();
        }
    }
}
