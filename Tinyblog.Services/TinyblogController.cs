using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tinyblog.Common.Log;
using Tinyblog.Contracts.Data;
using Tinyblog.Contracts.Services;
using Tinyblog.Services.Processors;

namespace Tinyblog.Services
{
    /// <summary>
    /// Implement ITinyblogService.
    /// </summary>
    /// <seealso cref="ITinyblogService" />
    public class TinyblogController: ITinyblogService
    {
        private readonly IArticleProcessor articleProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="TinyblogController"/> class.
        /// </summary>
        /// <param name="articleProcessor">The article processor.</param>
        public TinyblogController(IArticleProcessor articleProcessor)
        {
            this.articleProcessor = articleProcessor;
        }


        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns>ArticlePreviewInfo collection.</returns>
        public List<ArticlePreviewInfo> GetArticlePreviews()
        {
            try
            {
                return articleProcessor.GetArticlePreviews();
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }
    }
}
