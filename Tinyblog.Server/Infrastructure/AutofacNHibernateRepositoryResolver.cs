using System;
using Autofac;
using Tinyblog.DataLayer.Repository;

namespace Tinyblog.Server.Infrastructure
{
    public class AutofacRepositoryResolver : IRepositoryResolver
    {
        private readonly ILifetimeScope scope;

        public AutofacRepositoryResolver(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public T GetRepository<T, TDbContex>(TDbContex dbcontext)
        {
            return scope.Resolve<T>(new TypedParameter(typeof(TDbContex), dbcontext));
        }
    }
}
