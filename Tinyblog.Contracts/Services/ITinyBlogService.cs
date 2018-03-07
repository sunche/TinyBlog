using System;
using System.Collections.Generic;
using System.ServiceModel;
using Tinyblog.Contracts.Data;

namespace Tinyblog.Contracts.Services
{
    /// <summary>
    /// Interface for TinyblogService
    /// </summary>
    [ServiceContract]
    public interface ITinyblogService
    {
        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ArticlePreviewInfo> GetArticlePreviews();
    }
}
