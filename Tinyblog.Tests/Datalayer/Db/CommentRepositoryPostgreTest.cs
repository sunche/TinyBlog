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
    public class CommentRepositoryPostgreTest
    {
        private const string CreateCommentTableScriptName = "CreateCommentTable";
        private const string CreateDbScriptName = "CreateDb";
        private const string DbName = "TinyBlogCommentTest";
        private const string DropDbScriptName = "DropDb";
        private const string PostgreConnectionString = "Server=localhost;Port=5432;User Id=postgres;Password=";
        private readonly string dbConnectionString = $"Server=localhost;Port=5432;User Id=postgres;Password=;Database={DbName}";

        [OneTimeSetUp]
        public void TestSetup()
        {
            var createDbScript = string.Format(ScriptHelper.GetScriptFromResource(CreateDbScriptName), DbName);
            ScriptHelper.ExecuteScriptToPostgre(PostgreConnectionString, createDbScript);
            ScriptHelper.ExecuteScriptToPostgre(dbConnectionString, ScriptHelper.GetScriptFromResource(CreateCommentTableScriptName));
        }

        [Test]
        [Order(4)]
        public void CanAddCommentToPostgre()
        {
            var commentRepository = new CommentRepository(dbConnectionString);
            var commentId = Guid.NewGuid();
            var articleId = Guid.NewGuid();
            var commentText = "Test Text";
            var commentUserName = "Test user";

            commentRepository.Add(new Comment
            {
                Id = commentId,
                Text = commentText,
                Author = commentUserName,
                ArticleId = articleId
            });
            var comment = commentRepository.Get(commentId).Value;

            Assert.NotNull(comment);
            Assert.AreEqual(commentId, comment.Id);
            Assert.AreEqual(commentText, comment.Text);
            Assert.AreEqual(commentUserName, comment.Author);
            Assert.AreEqual(articleId, comment.ArticleId);
        }

        [Test]
        [Order(6)]
        public void CanDeleteCommentFromPostgre()
        {
            var commentRepository = new CommentRepository(dbConnectionString);
            var commentId = Guid.NewGuid();
            var articleId = Guid.NewGuid();
            var commentText = "Test Text";
            var commentUserName = "Test user";

            commentRepository.Add(new Comment
            {
                Id = commentId,
                Text = commentText,
                Author = commentUserName,
                ArticleId = articleId
            });
            Option<Comment> comment = commentRepository.Get(commentId);
            Assert.NotNull(comment);
            commentRepository.Delete(commentId);
            comment = commentRepository.Get(commentId);

            Assert.IsNull(comment);
        }

        [Test]
        [Order(5)]
        public void CanGetAllCommentsFromPostgre()
        {
            var commentRepository = new CommentRepository(dbConnectionString);
            var commentId = Guid.NewGuid();
            var articleId = Guid.NewGuid();
            var commentText = "Test Text";
            var commentUserName = "Test user";

            commentRepository.Add(new Comment
            {
                Id = commentId,
                Text = commentText,
                Author = commentUserName,
                ArticleId = articleId
            });
            IList<Comment> comments = commentRepository.GetAll();

            Assert.NotNull(comments);
            Assert.AreEqual(2, comments.Count);
            Assert.IsTrue(comments.Any(x => x.Id == commentId));
        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
            var deleteDbScript = string.Format(ScriptHelper.GetScriptFromResource(DropDbScriptName), DbName);
            ScriptHelper.ExecuteScriptToPostgre(PostgreConnectionString, deleteDbScript);
        }
    }
}
