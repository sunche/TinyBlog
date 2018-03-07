using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Tinyblog.Contracts.Data;
using Tinyblog.Contracts.Services;

namespace Tinyblog.Proxies.Shared
{
    /// <summary>
    /// Implements ITinyblogService.
    /// </summary>
    /// <seealso cref="System.ServiceModel.ClientBase{Tinyblog.Contracts.Services.ITinyblogService}" />
    /// <seealso cref="Tinyblog.Contracts.Services.ITinyblogService" />
    public class TinyblogClient : ClientBase<ITinyblogService>, ITinyblogService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TinyblogClient"/> class.
        /// </summary>
        /// <param name="endpoint">The endpoint.</param>
        public TinyblogClient(string endpoint)
            : base(endpoint)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TinyblogClient"/> class.
        /// </summary>
        /// <param name="binding">The binding.</param>
        /// <param name="address">The address.</param>
        public TinyblogClient(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }

        /// <summary>
        /// Gets the article previews.
        /// </summary>
        /// <returns>Collection of items.</returns>
        public List<ArticlePreviewInfo> GetArticlePreviews()
        {
            return Channel.GetArticlePreviews();
        }
    }
}
