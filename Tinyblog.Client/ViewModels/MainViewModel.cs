using System;
using System.Collections.ObjectModel;
using System.Linq;
using Nelibur.Sword.Extensions;
using Tinyblog.Client.Common;
using Tinyblog.Client.Services;
using Tinyblog.Contracts.Data;

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

        private CommentInfo editableComment;
        private bool isEditMode;

        private ArticleNavItemViewModel selectedArticleNavItem;

        private ArticleViewModel selectedArticleViewModel;

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
            AddArticleCommand = new Command(o => ShowAddArticleView());
            AddCommentCommand = new Command(o => ShowAddCommentView());
            CancelEditCommentCommand = new Command(o => CancelAnyEdit());
            ApplyAddCommentCommand = new Command(ValidateEditableComment, o => ApplyAddComment());
        }

        /// <summary>
        /// Gets the add article.
        /// </summary>
        public Command AddArticleCommand { get; }

        public Command AddCommentCommand { get; }

        public Command ApplyAddCommentCommand { get; }

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

        public Command CancelEditCommentCommand { get; }

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

        public string EditableCommentText
        {
            get => editableComment?.Text;
            set
            {
                editableComment.Text = value;
                OnPropertyChanged();
            }
        }

        public bool IsEditComment => editableComment != null;

        public bool IsEditMode
        {
            get => isEditMode;
            set
            {
                isEditMode = value;
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
            get => selectedArticleViewModel;
            set
            {
                if (selectedArticleViewModel == value)
                {
                    return;
                }

                selectedArticleViewModel = value;

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

        private void AddNewArticle(ArticleInfo article)
        {
            articleService.AddArticle(article);
            CancelAnyEdit();
            InitData();
        }

        private void ApplyAddComment()
        {
            articleService.AddComment(editableComment);
            CancelAnyEdit();
            SelectArticle(SelectedArticleNavItem);
        }

        private bool ValidateEditableComment(object obj)
        {
            return !string.IsNullOrEmpty(EditableCommentText);
        }

        private void CancelAnyEdit()
        {
            IsEditMode = false;
            editableComment = null;
            OnPropertyChanged(nameof(EditableCommentText));
            OnPropertyChanged(nameof(IsEditComment));
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

        private ArticleNavItemViewModel GetArtcilePreviewViewModel(ArticlePreviewInfo c)
        {
            return new ArticleNavItemViewModel(c, DeleteArticle);
        }

        private CommentViewModel GetCommentViewModel(CommentInfo c)
        {
            return new CommentViewModel(c, DeleteComment);
        }

        private void InitData()
        {
            ArticlesNav = new ObservableCollection<ArticleNavItemViewModel>
            (
                articleService.GetArticlePreviews()
                    .Select(c => GetArtcilePreviewViewModel(c))
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
                        .Select(c => GetCommentViewModel(c)));
                });
        }

        private void ShowAddArticleView()
        {
            var selectedArticleNav = SelectedArticleNavItem;
            SelectedArticleNavItem = null;
            SelectedArticle = new ArticleViewModel(AddNewArticle,
                () =>
                {
                    CancelAnyEdit();
                    SelectedArticleNavItem = selectedArticleNav;
                });
            IsEditMode = true;
        }

        private void ShowAddCommentView()
        {
            editableComment = new CommentInfo { Author = CurrentUser, ArticleId = SelectedArticleNavItem.Id };
            IsEditMode = true;
            OnPropertyChanged(nameof(EditableCommentText));
            OnPropertyChanged(nameof(IsEditComment));
        }
    }
}
