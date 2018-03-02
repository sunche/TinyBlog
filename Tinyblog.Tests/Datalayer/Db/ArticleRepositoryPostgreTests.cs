using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Tinyblog.DataLayer.Model;
using Tinyblog.DataLayer.Repository.Implementations;
using Tinyblog.Tests.Helpers;

namespace Tinyblog.Tests.Datalayer.Db
{
    [TestFixture]
    [Ignore("Requires a customized PostgreSql")]
    public class ArticleRepositoryPostgreTests
    {
        private const string DbName = "TinyBlogArticleTest";
        private const string CreateArticleTableScriptName = "CreateArticleTable";
        private const string CreateDbScriptName = "CreateDb";
        private const string DropDbScriptName = "DropDb";
        private const string PostgreConnectionString = "Server=localhost;Port=5432;User Id=postgres;Password=";
        private readonly string dbConnectionString = $"Server=localhost;Port=5432;User Id=postgres;Password=;Database={DbName}";

        [OneTimeSetUp]
        public void TestSetup()
        {
            var createDbScript = string.Format(ScriptHelper.GetScriptFromResource(CreateDbScriptName), DbName);
            ScriptHelper.ExecuteScriptToPostgre(PostgreConnectionString, createDbScript);
            ScriptHelper.ExecuteScriptToPostgre(dbConnectionString, ScriptHelper.GetScriptFromResource(CreateArticleTableScriptName));
        }

        [Test]
        [Order(1)]
        public void CanAddArticleToPostgre()
        {
            var articleRepository = new ArticleRepository(dbConnectionString);
            var articleId = Guid.NewGuid();
            var articleText = "Test Text";
            var articleTitle = "Test Title";

            articleRepository.Add(new Article { Id = articleId, Text = articleText, Title = articleTitle });
            var article = articleRepository.Get(articleId);

            Assert.NotNull(article);
            Assert.AreEqual(articleId, article.Id);
            Assert.AreEqual(articleText, article.Text);
            Assert.AreEqual(articleTitle, article.Title);
        }

        [Test]
        [Order(3)]
        public void CanDeleteArticlesFromPostgre()
        {
            var articleRepository = new ArticleRepository(dbConnectionString);
            var articleId = Guid.NewGuid();
            var articleText = "Test Text";
            var articleTitle = "Test Title";

            articleRepository.Add(new Article { Id = articleId, Text = articleText, Title = articleTitle });
            var article = articleRepository.Get(articleId);
            Assert.NotNull(article);
            articleRepository.Delete(articleId);
            article = articleRepository.Get(articleId);

            Assert.IsNull(article);
        }

        [Test]
        [Order(2)]
        public void CanGetAllArticlesFromPostgre()
        {
            var articleRepository = new ArticleRepository(dbConnectionString);
            var articleId = Guid.NewGuid();
            var articleText = "Test Text";
            var articleTitle = "Test Title";

            articleRepository.Add(new Article { Id = articleId, Text = articleText, Title = articleTitle });
            IList<Article> articles = articleRepository.GetAll();

            Assert.NotNull(articles);
            Assert.AreEqual(2, articles.Count);
            Assert.IsTrue(articles.Any(x => x.Id == articleId));
        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
            var deleteDbScript = string.Format(ScriptHelper.GetScriptFromResource(DropDbScriptName), DbName);
            ScriptHelper.ExecuteScriptToPostgre(PostgreConnectionString, deleteDbScript);
        }
    }
}
