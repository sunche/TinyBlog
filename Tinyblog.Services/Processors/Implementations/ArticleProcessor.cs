using System;
using System.Collections.Generic;
using System.Linq;
using Nelibur.Sword.Extensions;
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
        /// Deletes the article.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void DeleteArticle(Guid id)
        {
            articleRepository.Delete(id);
            commentRepository.GetForArticle(id)
                .ForEach(x => commentRepository.Delete(x.Id));
        }

        public void DeleteComment(Guid id)
        {
            commentRepository.Delete(id);
        }

        public ArticleInfo GetArticleInfo(Guid id)
        {
            return articleRepository.Get(id)
                .Map(x => Convert(x, commentRepository.GetForArticle(id))).Value;
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
            return new ArticlePreviewInfo
            {
                Id = article.Id,
                Title = article.Title,
                Author = article.Author
            };
        }

        private ArticleInfo Convert(Article article, List<Comment> comments)
        {
            return new ArticleInfo
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.Text,
                Author = article.Author,
                Comments = Convert(comments)
            };
        }

        private List<CommentInfo> Convert(List<Comment> comments)
        {
            return comments.Select(c =>
                new CommentInfo
                {
                    Id = c.Id,
                    Text = c.Text,
                    Author = c.Author
                }).ToList();
        }
    }
}
