using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using Tinyblog.DataLayer.Mapping;
using Tinyblog.DataLayer.Model;

namespace Tinyblog.DataLayer.Common
{
    public class NHibernateConfigurator
    {
        private static ISessionFactory sessionFactory;
        private readonly string connectionString;

        public NHibernateConfigurator(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ISessionFactory GetSessionFactory()
        {
            if (sessionFactory != null)
            {
                return sessionFactory;
            }

            return sessionFactory = GetConfiguration(connectionString).BuildSessionFactory();
        }

        private static Configuration GetConfiguration(string connectionString)
        {
            var configure = new Configuration().DataBaseIntegration(db =>
            {
                db.Dialect<PostgreSQL83Dialect>();
                db.Driver<NpgsqlDriver>();
                db.ConnectionString = connectionString;
            });

            var mapper = new ModelMapper();
            mapper.AddMapping<ArticleMapping>();
            mapper.AddMapping<CommentMapping>();
            HbmMapping mapping = mapper.CompileMappingFor(new[] { typeof(Article), typeof(Comment) });

            configure.AddMapping(mapping);

            //new SchemaUpdate(configure).Execute(true, true);

            return configure;
        }
    }
}
