using System;
using System.Collections.Generic;
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
        /// Adds the article.
        /// </summary>
        /// <param name="articleInfo">The article information.</param>
        /// <returns>
        /// Added article.
        /// </returns>
        public void AddArticle(ArticleInfo articleInfo)
        {
            articleRepository.Add(Convert(articleInfo));
        }

        public void AddComment(CommentInfo commentInfo)
        {
            commentRepository.Add(Convert(commentInfo));
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

        private static CommentInfo Convert(Comment comment)
        {
            return new CommentInfo
            {
                Id = comment.Id,
                Text = comment.Text,
                Author = comment.Author,
                ArticleId = comment.ArticleId
            };
        }

        private static Comment Convert(CommentInfo commentInfo)
        {
            return new Comment()
            {
                Id = commentInfo.Id,
                Text = commentInfo.Text,
                Author = commentInfo.Author,
                ArticleId = commentInfo.ArticleId
            };
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
            return comments.ConvertAll(Convert);
        }

        private Article Convert(ArticleInfo articleInfo)
        {
            return new Article()
            {
                Id = articleInfo.Id,
                Title = articleInfo.Title,
                Text = articleInfo.Text,
                Author = articleInfo.Author
            };
        }
    }
}
