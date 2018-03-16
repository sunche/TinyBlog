using System;
using NHibernate;
using Tinyblog.DataLayer.Model;

namespace Tinyblog.DataLayer.Repository.Implementations.NHibernate
{
    public class NhArticleRepository : NhRepositoryBase<Article>, IArticleRepository
    {
        public NhArticleRepository(ISession session) : base(session)
        {
        }
    }
}
