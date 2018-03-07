using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Autofac;
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
        private IArticleService articleService;
        private ObservableCollection<ArticleNavItemViewModel> articlesNav;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            ResolveDependencies();
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

        //TODO: перенести логику в app.cs.
        private void ResolveDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TinyblogModule());
            var container = builder.Build();
            articleService = container.Resolve<IArticleService>();
        }
    }
}
