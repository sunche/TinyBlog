using System;
using Tinyblog.DataLayer.Repository;

namespace Tinyblog.DataLayer.Core
{
    public interface IUnitOfWork : IDisposable
    {
        T GetRepository<T>();

        void Commit();
    }
}
