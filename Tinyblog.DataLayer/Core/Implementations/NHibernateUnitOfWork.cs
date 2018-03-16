using System;
using NHibernate;
using Tinyblog.DataLayer.Repository;

namespace Tinyblog.DataLayer.Core.Implementations
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        private readonly IRepositoryResolver repositoryResolver;
        private readonly ISession session;

        public NHibernateUnitOfWork(ISession session, IRepositoryResolver repositoryResolver)
        {
            this.session = session;
            session.BeginTransaction();
            this.repositoryResolver = repositoryResolver;
        }

        public void Commit()
        {
            try
            {
                if (session.Transaction != null && session.Transaction.IsActive)
                {
                    session.Transaction.Commit();
                }
            }
            catch
            {
                if (session.Transaction != null && session.Transaction.IsActive)
                {
                    session.Transaction.Rollback();
                }

                throw;
            }
            finally
            {
                session.Close();
            }
        }

        public void Dispose()
        {
            if (!session.IsOpen)
            {
                return;
            }

            if (session.Transaction != null && session.Transaction.IsActive)
            {
                session.Transaction.Rollback();
            }

            session.Close();
        }

        public T GetRepository<T>()
        {
            return repositoryResolver.GetRepository<T, ISession>(session);
        }
    }
}
