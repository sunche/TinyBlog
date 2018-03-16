using System;
using System.Collections.Generic;
using System.Linq;
using Nelibur.Sword.Extensions;
using Tinyblog.Contracts.Data;
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
        private readonly ICommentRepository commentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleProcessor"/> class.
        /// </summary>
        /// <param name="articleRepository">The article repository.</param>
        /// <param name="commentRepository">The comment repository.</param>
        public ArticleProcessor(IArticleRepository articleRepository, ICommentRepository commentRepository)
        {
            this.articleRepository = articleRepository;
            this.commentRepository = commentRepository;
        }

        /// <summary>
        /// Adds the article.
        /// </summary>
        /// <param name="articleInfo">The article information.</param>
        /// <returns>
        /// Added article.
        /// </returns>
        public void AddArticle(ArticleInfo articleInfo)
        {
            articleRepository.Add(ArticleMapper.Convert(articleInfo));
        }

        public void AddComment(CommentInfo commentInfo)
        {
            commentRepository.Add(ArticleMapper.Convert(commentInfo));
        }

        /// <summary>
        /// Deletes the article.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteArticle(Guid id)
        {
            articleRepository.Delete(id);
            Guid[] idsToDelete = commentRepository.GetForArticle(id)
                .Select(x => x.Id).ToArray();
            commentRepository.Delete(idsToDelete);
        }

        public void DeleteComment(Guid id)
        {
            commentRepository.Delete(id);
        }

        public ArticleInfo GetArticleInfo(Guid id)
        {
            return articleRepository.Get(id)
                .Map(x => ArticleMapper.Convert(x, commentRepository.GetForArticle(id))).Value;
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
                .ConvertAll(ArticleMapper.Convert);
        }
    }
}
