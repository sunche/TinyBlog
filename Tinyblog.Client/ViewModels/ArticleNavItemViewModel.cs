using System;
using Tinyblog.Contracts.Data;

namespace Tinyblog.Client.ViewModels
{
    /// <summary>
    /// ViewModel for article nav item.
    /// </summary>
    /// <seealso cref="Tinyblog.Client.ViewModels.ViewModelBase"/>
    public class ArticleNavItemViewModel : ViewModelBase
    {
        private readonly ArticlePreviewInfo articlePreviewInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleNavItemViewModel"/> class.
        /// </summary>
        /// <param name="articlePreviewInfo">The article preview information.</param>
        public ArticleNavItemViewModel(ArticlePreviewInfo articlePreviewInfo)
        {
            this.articlePreviewInfo = articlePreviewInfo;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title => articlePreviewInfo.Title;
    }
}
