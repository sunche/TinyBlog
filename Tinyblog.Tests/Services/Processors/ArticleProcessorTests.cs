using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
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
        public void GetArticlePreviewsOk()
        {
            var mockArticleRepository = new Mock<IArticleRepository>();
            var firstArticleId = Guid.NewGuid();
            var firstArticleTitle = "first article title";
            var firstArticleText = "first article text";
            var secondArticleId = Guid.NewGuid();
            var secondArticleTitle = "second article title";
            var secondArticleText = "second article text";
            mockArticleRepository.Setup(x => x.GetAll()).Returns(new List<Article>
            {
                new Article { Id = firstArticleId, Title = firstArticleTitle, Text = firstArticleText },
                new Article { Id = secondArticleId, Title = secondArticleTitle, Text = secondArticleText },
            });
            var articleProcessor = new ArticleProcessor(mockArticleRepository.Object);

            IEnumerable<ArticlePreviewInfo> result = articleProcessor.GetArticlePreviews();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(firstArticleTitle, result.First(x => x.Id == firstArticleId).Title);
            Assert.AreEqual(secondArticleTitle, result.First(x => x.Id == secondArticleId).Title);
        }
    }
}
