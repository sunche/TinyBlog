using System;
using System.Collections.Generic;
using Tinyblog.DataLayer.Model;

namespace Tinyblog.DataLayer.Repository.Implementations
{
    /// <summary>
    /// Article Repository.
    /// </summary>
    /// <seealso cref="Tinyblog.DataLayer.Repository.Implementations.RepositoryBase{Tinyblog.DataLayer.Model.Article}"/>
    /// <seealso cref="Tinyblog.DataLayer.Repository.IArticleRepository"/>
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public ArticleRepository(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Gets the name of the table for current entity.
        /// </summary>
        /// <returns>
        /// Table name.
        /// </returns>
        protected override string TableName => "article";

        /// <summary>
        /// Gets the insert script for entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Insert script.
        /// </returns>
        protected override KeyValuePair<string, object> GetInsertScript(Article entity)
        {
            return new KeyValuePair<string, object>(
                $"INSERT INTO {TableName} (id,text,title, author) VALUES((@Id),(@Text),(@Title),(@Author))",
                new { entity.Id, entity.Text, entity.Title, entity.Author });
        }
    }
}
