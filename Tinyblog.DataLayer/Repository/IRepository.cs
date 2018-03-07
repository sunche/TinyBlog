using System;
using System.Collections.Generic;

namespace Tinyblog.DataLayer.Repository
{
    /// <summary>
    /// Repository interface.
    /// </summary>
    /// <typeparam name="T">Entity.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(T entity);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(Guid id);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity.</returns>
        T Get(Guid id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>Entities' collection.</returns>
        List<T> GetAll();
    }
}
