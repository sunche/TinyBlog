using System;
using System.Collections.Generic;
using Tinyblog.DataLayer.Model;

namespace Tinyblog.DataLayer.Repository
{
    /// <summary>
    /// Repo interface for comment entity.
    /// </summary>
    /// <seealso cref="Tinyblog.DataLayer.Repository.IRepository{Tinyblog.DataLayer.Model.Comment}" />
    public interface ICommentRepository : IRepository<Comment>
    {
        /// <summary>
        /// Get collection of comments for the article.
        /// </summary>
        /// <param name="articleId">The article identifier.</param>
        /// <returns>Collection of comments.</returns>
        List<Comment> GetForArticle(Guid articleId);
    }
}
