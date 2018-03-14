using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Nelibur.Sword.DataStructures;
using Nelibur.Sword.Extensions;
using NUnit.Framework;
using Tinyblog.Contracts.Data;
using Tinyblog.DataLayer.Model;
using Tinyblog.DataLayer.Repository;
using Tinyblog.Services.Processors.Implementations;

namespace Tinyblog.Tests.Services.Processors
{
    [TestFixture]
    public class ArticleProcessorTests
    {
        [Test]
        public void GetArticleOk()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();
            var articleId = Guid.NewGuid();
            var articleTitle = "article title";
            var articleText = "article text";
            var articleAuthor = "bob";
            mockArticleRepository.Setup(x => x.Get(articleId)).Returns(
                new Article
                {
                    Id = articleId,
                    Title = articleTitle,
                    Text = articleText,
                    Author = articleAuthor
                }.ToOption()
            );

            var mockCommentsRepository = new Mock<ICommentRepository>();
            var commentId = Guid.NewGuid();
            var commentText = "comment text";
            var commentAuthor = "bob";

            mockCommentsRepository.Setup(x => x.GetForArticle(articleId)).Returns(
                new List<Comment>
                {
                    new Comment
                    {
                        Id = commentId,
                        ArticleId = articleId,
                        Text = commentText,
                        Author = commentAuthor
                    }
                }
            );

            var articleProcessor = new ArticleProcessor(mockArticleRepository.Object, mockCommentsRepository.Object);

            ArticleInfo result = articleProcessor.GetArticleInfo(articleId);
            var resultComment = result.Comments.FirstOrDefault();

            Assert.NotNull(result);
            Assert.AreEqual(articleTitle, result.Title);
            Assert.AreEqual(articleAuthor, result.Author);
            Assert.AreEqual(articleText, result.Text);
            Assert.AreEqual(1, result.Comments.Count);
            Assert.NotNull(resultComment);
            Assert.AreEqual(commentText, resultComment.Text);
            Assert.AreEqual(commentAuthor, resultComment.Author);
        }

        [Test]
        public void GetArticlePreviewsOk()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();
            var firstArticleId = Guid.NewGuid();
            var firstArticleTitle = "first article title";
            var firstArticleText = "first article text";
            var firstArticleAuthor = "Bob";
            var secondArticleId = Guid.NewGuid();
            var secondArticleTitle = "second article title";
            var secondArticleText = "second article text";
            var secondArticleAuthor = "Cate";
            mockArticleRepository.Setup(x => x.GetAll()).Returns(new List<Article>
            {
                new Article
                {
                    Id = firstArticleId,
                    Title = firstArticleTitle,
                    Text = firstArticleText,
                    Author = firstArticleAuthor
                },
                new Article
                {
                    Id = secondArticleId,
                    Title = secondArticleTitle,
                    Text = secondArticleText,
                    Author = secondArticleAuthor
                },
            });
            var articleProcessor = new ArticleProcessor(mockArticleRepository.Object, null);

            IEnumerable<ArticlePreviewInfo> result = articleProcessor.GetArticlePreviews();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(firstArticleTitle, result.First(x => x.Id == firstArticleId).Title);
            Assert.AreEqual(secondArticleTitle, result.First(x => x.Id == secondArticleId).Title);
            Assert.AreEqual(firstArticleAuthor, result.First(x => x.Id == firstArticleId).Author);
            Assert.AreEqual(secondArticleAuthor, result.First(x => x.Id == secondArticleId).Author);
        }

        [Test]
        public void GetArticleWithoutCommentsOk()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();
            var articleId = Guid.NewGuid();
            var articleTitle = "article title";
            var articleText = "article text";
            var articleAuthor = "bob";
            mockArticleRepository.Setup(x => x.Get(articleId)).Returns(
                new Article
                {
                    Id = articleId,
                    Title = articleTitle,
                    Text = articleText,
                    Author = articleAuthor
                }.ToOption()
            );

            var mockCommentsRepository = new Mock<ICommentRepository>();
            mockCommentsRepository.Setup(x => x.GetForArticle(articleId))
                .Returns(new List<Comment>());

            var articleProcessor = new ArticleProcessor(mockArticleRepository.Object, mockCommentsRepository.Object);

            ArticleInfo result = articleProcessor.GetArticleInfo(articleId);

            Assert.NotNull(result);
            Assert.NotNull(result.Comments);
            Assert.AreEqual(0, result.Comments.Count);
        }

        [Test]
        public void GetNotExistArticleReturnNull()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();
            var articleId = Guid.NewGuid();
            mockArticleRepository.Setup(x => x.Get(articleId))
                .Returns(Option<Article>.Empty);

            var articleProcessor = new ArticleProcessor(mockArticleRepository.Object, null);

            ArticleInfo result = articleProcessor.GetArticleInfo(articleId);

            Assert.IsNull(result);
        }
    }
}
