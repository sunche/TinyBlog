using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Tinyblog.DataLayer.Model;

namespace Tinyblog.DataLayer.Mapping
{
    public class CommentMapping : ClassMapping<Comment>
    {
        public CommentMapping()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Text);
            Property(x => x.ArticleId);
            Property(x => x.Author);
        }
    }
}
