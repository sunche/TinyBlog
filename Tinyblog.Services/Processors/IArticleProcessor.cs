using System.Collections.Generic;
using Tinyblog.Contracts.Data;

namespace Tinyblog.Services.Processors
{
    /// <summary>
    /// Interface to work with articles.
    /// </summary>
    public interface IArticleProcessor
    {
        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns>Article preview collection.</returns>
        List<ArticlePreviewInfo> GetArticlePreviews();
    }
}
