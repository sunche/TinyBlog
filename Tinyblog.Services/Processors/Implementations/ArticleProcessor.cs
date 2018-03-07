using System;
using System.Collections.Generic;
using System.Linq;
using Tinyblog.Contracts.Data;
using Tinyblog.DataLayer.Model;
using Tinyblog.DataLayer.Repository;

namespace Tinyblog.Services.Processors.Implementations
{
    /// <summary>
    /// Implementation of the IArticleProcessor.
    /// </summary>
    /// <seealso cref="IArticleProcessor"/>
    public class ArticleProcessor : IArticleProcessor
    {
        private readonly IArticleRepository articleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleProcessor"/> class.
        /// </summary>
        /// <param name="articleRepository">The article repository.</param>
        public ArticleProcessor(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns>
        /// Items collection.
        /// </returns>
        public List<ArticlePreviewInfo> GetArticlePreviews()
        {
            return articleRepository.GetAll()
                .ConvertAll(Convert);
        }

        private ArticlePreviewInfo Convert(Article article)
        {
            return new ArticlePreviewInfo { Id = article.Id, Title = article.Title };
        }
    }
}
