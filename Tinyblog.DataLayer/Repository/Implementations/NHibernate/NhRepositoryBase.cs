using System;
using System.Collections.Generic;
using System.Linq;
using Nelibur.Sword.DataStructures;
using Nelibur.Sword.Extensions;
using NHibernate;
using NHibernate.Linq;
using Tinyblog.DataLayer.Model;

namespace Tinyblog.DataLayer.Repository.Implementations.NHibernate
{
    public class NhRepositoryBase<T> : IRepository<T>
        where T : BaseEntity
    {
        protected readonly ISession Session;

        public NhRepositoryBase(ISession session)
        {
            Session = session;
        }

        public void Add(T entity)
        {
            Session.Save(entity);
        }

        //TODO: проверить.
        public void Delete(Guid id)
        {
            Session.Query<T>().Where(x => x.Id == id).Delete();
        }

        public Option<T> Get(Guid id)
        {
            return Session.Get<T>(id).ToOption();
        }

        public List<T> GetAll()
        {
            return Session.Query<T>().ToList();
        }
    }
}
