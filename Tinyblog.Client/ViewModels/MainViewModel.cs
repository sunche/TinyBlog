using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            }
        }

        private void InitData()
        {
            IEnumerable<ArticlePreviewInfo> articles = articleService.GetArticlePreviews();
            ArticlesNav = new ObservableCollection<ArticleNavItemViewModel>(articles.Select(x => new ArticleNavItemViewModel(x)));
        }
    }
}
