using System;
using System.Collections.Generic;
using Tinyblog.Contracts.Data;
using Tinyblog.DataLayer.Model;

namespace Tinyblog.Services.Processors.Implementations
{
    public class ArticleMapper
    {
        public static Comment Convert(CommentInfo commentInfo)
        {
            return new Comment()
            {
                Id = commentInfo.Id,
                Text = commentInfo.Text,
                Author = commentInfo.Author,
                ArticleId = commentInfo.ArticleId
            };
        }

        public static ArticlePreviewInfo Convert(Article article)
        {
            return new ArticlePreviewInfo
            {
                Id = article.Id,
                Title = article.Title,
                Author = article.Author
            };
        }

        public static ArticleInfo Convert(Article article, List<Comment> comments)
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

        public static Article Convert(ArticleInfo articleInfo)
        {
            return new Article()
            {
                Id = articleInfo.Id,
                Title = articleInfo.Title,
                Text = articleInfo.Text,
                Author = articleInfo.Author
            };
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

        private static List<CommentInfo> Convert(List<Comment> comments)
        {
            return comments.ConvertAll(Convert);
        }
    }
}
