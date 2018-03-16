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
        [Order(1)]
        public void CanAddCommentToPostgre()
        {
            var commentRepository = new CommentRepository(dbConnectionString);
            var articleId = Guid.NewGuid();
            var commentText = "Test Text";
            var commentUserName = "Test user";

            commentRepository.Add(new Comment
            {
                Text = commentText,
                Author = commentUserName,
                ArticleId = articleId
            });
            List<Comment> comments = commentRepository.GetAll();
            Assert.IsNotEmpty(comments);

            var comment = comments.First();

            Assert.NotNull(comment);
            Assert.AreEqual(commentText, comment.Text);
            Assert.AreEqual(commentUserName, comment.Author);
            Assert.AreEqual(articleId, comment.ArticleId);
        }

        [Test]
        [Order(3)]
        public void CanDeleteCommentFromPostgre()
        {
            var commentRepository = new CommentRepository(dbConnectionString);
            var articleId = Guid.NewGuid();
            var commentText = "Test Text";
            var commentUserName = "Test user";

            commentRepository.Add(new Comment
            {
                Text = commentText,
                Author = commentUserName,
                ArticleId = articleId
            });
            List<Comment> comments = commentRepository.GetAll();
            Assert.IsNotEmpty(comments);
            var comment = comments.Last();
            Assert.NotNull(comment);
            commentRepository.Delete(comment.Id);
            comment = commentRepository.Get(comment.Id).Value;

            Assert.IsNull(comment);
        }


        [Test]
        [Order(4)]
        public void CanDeleteCommentsFromPostgre()
        {
            var commentRepository = new CommentRepository(dbConnectionString);
            var articleId = Guid.NewGuid();
            var commentText = "Test Text";
            var commentUserName = "Test user";

            var secondcommentText = "Second Test Text";
            var secondcommentUserName = "Second Test user";

            commentRepository.Add(new Comment
            {
                Text = commentText,
                Author = commentUserName,
                ArticleId = articleId
            });

            commentRepository.Add(new Comment
            {
                Text = secondcommentText,
                Author = secondcommentUserName,
                ArticleId = articleId
            });

            List<Comment> comments = commentRepository.GetAll();
            Assert.IsNotEmpty(comments);

            comments.Reverse();
            List<Guid> commentsToDelete = comments.Take(2).Select(x=>x.Id).ToList();

            commentRepository.Delete(commentsToDelete);
            Comment comment = commentRepository.Get(commentsToDelete.First()).Value;

            Assert.IsNull(comment);
        }

        [Test]
        [Order(2)]
        public void CanGetAllCommentsFromPostgre()
        {
            var commentRepository = new CommentRepository(dbConnectionString);
            var articleId = Guid.NewGuid();
            var commentText = Guid.NewGuid().ToString();
            var commentUserName = "Test user";

            commentRepository.Add(new Comment
            {
                Text = commentText,
                Author = commentUserName,
                ArticleId = articleId
            });
            IList<Comment> comments = commentRepository.GetAll();

            Assert.NotNull(comments);
            Assert.AreEqual(2, comments.Count);
            Assert.IsTrue(comments.Any(x => x.Text == commentText));
        }

        [OneTimeTearDown]
        public void TestTearDown()
        {
            var deleteDbScript = string.Format(ScriptHelper.GetScriptFromResource(DropDbScriptName), DbName);
            ScriptHelper.ExecuteScriptToPostgre(PostgreConnectionString, deleteDbScript);
        }
    }
}
