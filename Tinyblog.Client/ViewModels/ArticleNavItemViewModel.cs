using System;
using Tinyblog.Client.Common;
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

        [Obsolete]
        public ArticleNavItemViewModel()
        {
            articlePreviewInfo = new ArticlePreviewInfo();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleNavItemViewModel"/> class.
        /// </summary>
        /// <param name="articlePreviewInfo">The article preview information.</param>
        /// <param name="deleteArticleAction"></param>
        public ArticleNavItemViewModel(ArticlePreviewInfo articlePreviewInfo, Action<object> deleteArticleAction)
        {
            this.articlePreviewInfo = articlePreviewInfo;
            DeleteArticleCommand = new Command(CanDeleteArticle, deleteArticleAction);
        }

        /// <summary>
        /// Gets or sets the delete article command.
        /// </summary>
        public Command DeleteArticleCommand { get; }
        
        /// <summary>
        /// Gets the article identifier.
        /// </summary>
        public Guid Id => articlePreviewInfo.Id;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title => articlePreviewInfo.Title;

        private bool CanDeleteArticle(object obj)
        {
            return articlePreviewInfo.Author == this.CurrentUser;
        }
    }
}
