using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Tinyblog.DataLayer.Model;

namespace Tinyblog.DataLayer.Repository.Implementations.NHibernate
{
    public class NhCommentRepository : NhRepositoryBase<Comment>, ICommentRepository
    {
        public NhCommentRepository(ISession session) : base(session)
        {
        }

        public List<Comment> GetForArticle(Guid articleId)
        {
            return Session.Query<Comment>().Where(x => x.ArticleId == articleId).ToList();
        }
    }
}
