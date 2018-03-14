using System;
using System.Collections.ObjectModel;
using System.Linq;
using Nelibur.Sword.Extensions;
using Tinyblog.Client.Common;
using Tinyblog.Client.Services;

namespace Tinyblog.Client.ViewModels
{
    /// <summary>
    /// Main viewmodel
    /// </summary>
    /// <seealso cref="Tinyblog.Client.ViewModels.ViewModelBase"/>
    public class MainViewModel : ViewModelBase
    {
        private readonly IArticleService articleService;
        private ObservableCollection<ArticleNavItemViewModel> articlesNav;
        private ObservableCollection<CommentViewModel> comments;

        private ArticleViewModel selectedArticle;

        private ArticleNavItemViewModel selectedArticleNavItem;

        [Obsolete]
        public MainViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel(IArticleService service)
        {
            articleService = service;
            InitData();
            ReloadCommand = new Command(o => InitData());
        }

        /// <summary>
        /// Gets or sets the articles nav.
        /// </summary>
        public ObservableCollection<ArticleNavItemViewModel> ArticlesNav
        {
            get => articlesNav;
            set
            {
                articlesNav = value;
                OnPropertyChanged();
                if (articlesNav.Any())
                {
                    SelectedArticleNavItem = articlesNav.First();
                }
            }
        }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        public ObservableCollection<CommentViewModel> Comments
        {
            get => comments;
            set
            {
                comments = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the reload command.
        /// </summary>
        public Command ReloadCommand { get; }

        /// <summary>
        /// Gets or sets the selected article.
        /// </summary>
        public ArticleViewModel SelectedArticle
        {
            get => selectedArticle;
            set
            {
                if (selectedArticle == value)
                {
                    return;
                }

                selectedArticle = value;

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Selected Article Nav item.
        /// </summary>
        public ArticleNavItemViewModel SelectedArticleNavItem
        {
            get => selectedArticleNavItem;
            set
            {
                if (selectedArticleNavItem == value)
                {
                    return;
                }

                selectedArticleNavItem = value;

                SelectArticle(selectedArticleNavItem);
                OnPropertyChanged();
            }
        }

        private void DeleteArticle(object obj)
        {
            if (!Guid.TryParse(obj.ToString(), out Guid articleId))
            {
                return;
            }

            articleService.DeleteArticle(articleId);
            InitData();
        }

        private void DeleteComment(object obj)
        {
            if (!Guid.TryParse(obj.ToString(), out Guid commentId))
            {
                return;
            }

            articleService.DeleteComment(commentId);
            var comment = comments.FirstOrDefault(x => x.Id == commentId);
            comments.Remove(comment);
        }

        private void InitData()
        {
            ArticlesNav = new ObservableCollection<ArticleNavItemViewModel>
            (
                articleService.GetArticlePreviews()
                    .Select(c => new ArticleNavItemViewModel(c, DeleteArticle))
            );
        }

        private void SelectArticle(ArticleNavItemViewModel articleInfo)
        {
            if (articleInfo == null)
            {
                SelectedArticle = null;
                Comments = null;
                return;
            }

            articleService.GetArticleInfo(articleInfo.Id)
                .ToOption()
                .Do(x =>
                {
                    SelectedArticle = new ArticleViewModel(x);
                    Comments = new ObservableCollection<CommentViewModel>(x.Comments
                        .Select(c => new CommentViewModel(c, DeleteComment)));
                });
        }
    }
}
