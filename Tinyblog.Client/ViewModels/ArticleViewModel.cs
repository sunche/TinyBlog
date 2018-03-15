using System;
using Tinyblog.Client.Common;
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
        private bool isEditMode;

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
        /// Initializes a new instance of the <see cref="ArticleViewModel"/> class.
        /// </summary>
        /// <param name="addArticleAction"></param>
        /// <param name="cancelEditAction"></param>
        public ArticleViewModel(Action<ArticleInfo> addArticleAction, Action cancelEditAction)
        {
            articleInfo = new ArticleInfo { Author = CurrentUser };
            IsEditMode = true;

            ApplyEditCommand = new Command(
                ValidateArticle,
                o => { addArticleAction(articleInfo); });

            CancelEditCommand = new Command(o => { cancelEditAction(); });
        }

        public Command ApplyEditCommand { get; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Author => articleInfo.Author;

        public Command CancelEditCommand { get; }

        public bool IsEditMode
        {
            get => isEditMode;
            private set
            {
                isEditMode = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text
        {
            get => articleInfo.Text;
            set
            {
                articleInfo.Text = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get => articleInfo.Title;
            set
            {
                articleInfo.Title = value;
                OnPropertyChanged();
            }
        }

        private bool ValidateArticle(object obj)
        {
            return !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Text);
        }
    }
}
