using System;

namespace Tinyblog.DataLayer.Repository
{
    public interface IRepositoryResolver
    {
        T GetRepository<T, TDbContex>(TDbContex dbcontext);
    }
}
