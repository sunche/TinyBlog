using Tinyblog.DataLayer.Common;
using Tinyblog.DataLayer.Repository;

namespace Tinyblog.DataLayer.Core.Implementations
{
    public class NHibernateUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string connectionString;
        private NHibernateConfigurator configurator;
        private readonly IRepositoryResolver repositoryResolver;

        private NHibernateConfigurator Configurator => configurator ?? (configurator = new NHibernateConfigurator(connectionString));

        public NHibernateUnitOfWorkFactory(string connectionString, IRepositoryResolver repositoryResolver)
        {
            this.connectionString = connectionString;
            this.repositoryResolver = repositoryResolver;
        }
        public IUnitOfWork Create()
        {
            return new NHibernateUnitOfWork(Configurator.GetSessionFactory().OpenSession(), repositoryResolver);
        }
    }
}