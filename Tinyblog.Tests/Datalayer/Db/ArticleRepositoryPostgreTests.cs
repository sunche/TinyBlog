using System;
using System.Collections.Generic;
using System.Linq;
using Nelibur.Sword.DataStructures;
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
        private const string CreateArticleTableScriptName = "CreateArticleTable";
        private const string CreateDbScriptName = "CreateDb";
        private const string DbName = "TinyBlogArticleTest";
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
            var articleText = "Test Text";
            var articleTitle = "Test Title";
            var articleAuthor = "Bob";

            articleRepository.Add(new Article
            {
                Text = articleText,
                Title = articleTitle,
                Author = articleAuthor
            });

            List<Article> articles = articleRepository.GetAll();
            Assert.IsNotEmpty(articles);

            var article = articleRepository.Get(articles.First().Id).Value;

            Assert.AreEqual(articleText, article.Text);
            Assert.AreEqual(articleTitle, article.Title);
            Assert.AreEqual(articleAuthor, article.Author);
        }

        [Test]
        [Order(3)]
        public void CanDeleteArticlesFromPostgre()
        {
            var articleRepository = new ArticleRepository(dbConnectionString);
            var articleText = "Test Text";
            var articleTitle = "Test Title";
            var articleAuthor = "Bob";

            articleRepository.Add(new Article
            {
                Text = articleText,
                Title = articleTitle,
                Author = articleAuthor
            });
            Article article = articleRepository.GetAll().LastOrDefault();
            Assert.NotNull(article);
            articleRepository.Delete(article.Id);
            article = articleRepository.Get(article.Id).Value;

            Assert.IsNull(article);
        }

        [Test]
        [Order(2)]
        public void CanGetAllArticlesFromPostgre()
        {
            var articleRepository = new ArticleRepository(dbConnectionString);
            var articleText = "Test Text";
            var articleTitle = Guid.NewGuid().ToString();
            var articleAuthor = "Bob";

            articleRepository.Add(new Article
            {
                Text = articleText,
                Title = articleTitle,
                Author = articleAuthor
            });
            IList<Article> articles = articleRepository.GetAll();

            Assert.NotNull(articles);
            Assert.AreEqual(2, articles.Count);
            Assert.IsTrue(articles.Any(x => x.Title == articleTitle));
        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
            var deleteDbScript = string.Format(ScriptHelper.GetScriptFromResource(DropDbScriptName), DbName);
            ScriptHelper.ExecuteScriptToPostgre(PostgreConnectionString, deleteDbScript);
        }

        [Test]
        [Order(4)]
        public void TryDeleteNotExistArticlesFromPostgreNotThrowTheError()
        {
            var articleRepository = new ArticleRepository(dbConnectionString);
            var articleId = Guid.NewGuid();

            Assert.DoesNotThrow(() => articleRepository.Delete(articleId));
        }
    }
}
