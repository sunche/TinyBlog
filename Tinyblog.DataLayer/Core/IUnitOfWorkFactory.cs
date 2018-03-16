using System;

namespace Tinyblog.DataLayer.Core
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
