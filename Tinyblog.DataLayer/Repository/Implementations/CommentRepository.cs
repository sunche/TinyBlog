using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Tinyblog.DataLayer.Model;

namespace Tinyblog.DataLayer.Repository.Implementations
{
    /// <summary>
    /// Comments repository.
    /// </summary>
    /// <seealso cref="Tinyblog.DataLayer.Repository.Implementations.RepositoryBase{Tinyblog.DataLayer.Model.Comment}"/>
    /// <seealso cref="Tinyblog.DataLayer.Repository.ICommentRepository"/>
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public CommentRepository(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Gets the name of the table for current entity.
        /// </summary>
        /// <returns>
        /// Table name.
        /// </returns>
        protected override string TableName => "comment";

        /// <summary>
        /// Get collection of comments for the article.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <returns>
        /// Collection of comments.
        /// </returns>
        public List<Comment> GetForArticle(Guid articleId)
        {
            using (IDbConnection db = GetConnection())
            {
                return db.Query<Comment>($"SELECT * FROM {TableName} WHERE articleId = (@articleId)", new { articleId }).ToList();
            }
        }

        /// <summary>
        /// Gets the insert script for entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Insert script.
        /// </returns>
        protected override KeyValuePair<string, object> GetInsertScript(Comment entity)
        {
            return new KeyValuePair<string, object>(
                $"INSERT INTO {TableName} (id,articleId,text,author) VALUES((@Id),(@ArticleId),(@Text),(@Author))",
                new { entity.Id, entity.ArticleId, entity.Text, entity.Author });
        }
    }
}
