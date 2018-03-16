using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Tinyblog.DataLayer.Model;

namespace Tinyblog.DataLayer.Mapping
{
    public class ArticleMapping : ClassMapping<Article>
    {
        public ArticleMapping()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Text);
            Property(x => x.Title);
            Property(x => x.Author);
        }
    }
}
