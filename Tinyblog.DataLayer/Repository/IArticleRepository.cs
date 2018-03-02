using Tinyblog.DataLayer.Model;

namespace Tinyblog.DataLayer.Repository
{
    /// <summary>
    /// Repo interface for article entity.
    /// </summary>
    /// <seealso cref="Tinyblog.DataLayer.Repository.IRepository{Tinyblog.DataLayer.Model.Article}" />
    public interface IArticleRepository: IRepository<Article>
    {
        
    }
}
