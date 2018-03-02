using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Npgsql;

namespace Tinyblog.DataLayer.Repository.Implementations
{
    /// <summary>
    /// Base class for repo.
    /// </summary>
    /// <typeparam name="T">Type of entity.</typeparam>
    /// <seealso cref="Tinyblog.DataLayer.Repository.IRepository{T}"/>
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public RepositoryBase(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Gets the name of the table for current entity.
        /// </summary>
        /// <returns>Table name.</returns>
        protected abstract string TableName { get; }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(T entity)
        {
            using (IDbConnection db = GetConnection())
            {
                db.Execute(GetInsertScript(entity));
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(Guid id)
        {
            using (IDbConnection db = GetConnection())
            {
                db.Execute($"DELETE FROM {TableName} WHERE Id='{id}'");
            }
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Entity.
        /// </returns>
        public T Get(Guid id)
        {
            using (IDbConnection db = GetConnection())
            {
                return db.QueryFirstOrDefault<T>($"SELECT * FROM {TableName} WHERE id = '{id}'");
            }
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// Entities' collection.
        /// </returns>
        public IList<T> GetAll()
        {
            using (IDbConnection db = GetConnection())
            {
                return db.Query<T>($"SELECT * FROM {TableName}").ToList();
            }
        }

        protected IDbConnection GetConnection()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Gets the insert script for entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Insert script.</returns>
        protected abstract string GetInsertScript(T entity);
    }
}
