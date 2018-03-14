using System;
using Tinyblog.Contracts.Data;

namespace Tinyblog.Client.ViewModels
{
    /// <summary>
    /// ViewModel for article view.
    /// </summary>
    /// <seealso cref="Tinyblog.Client.ViewModels.ViewModelBase"/>
    public class ArticleViewModel : ViewModelBase
    {
        private readonly ArticleInfo articleInfo;

        [Obsolete]
        public ArticleViewModel()
        {
            articleInfo = new ArticleInfo();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleViewModel"/> class.
        /// </summary>
        /// <param name="articleInfo">The article information.</param>
        public ArticleViewModel(ArticleInfo articleInfo)
        {
            this.articleInfo = articleInfo;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Author => articleInfo.Author;

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text => articleInfo.Text;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title => articleInfo.Title;
    }
}
