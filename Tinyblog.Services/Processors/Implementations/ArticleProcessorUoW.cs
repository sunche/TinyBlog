using System;
using System.Collections.Generic;
using System.Linq;
using Nelibur.Sword.Extensions;
using Tinyblog.Contracts.Data;
using Tinyblog.DataLayer.Core;
using Tinyblog.DataLayer.Repository;

namespace Tinyblog.Services.Processors.Implementations
{
    public class ArticleProcessorUoW : IArticleProcessor
    {
        private readonly IUnitOfWorkFactory unitOfWorkFactory;

        public ArticleProcessorUoW(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public void AddArticle(ArticleInfo articleInfo)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.GetRepository<IArticleRepository>()
                    .Add(ArticleMapper.Convert(articleInfo));
                unitOfWork.Commit();
            }
        }

        public void AddComment(CommentInfo commentInfo)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.GetRepository<ICommentRepository>()
                    .Add(ArticleMapper.Convert(commentInfo));
                unitOfWork.Commit();
            }
        }

        public void DeleteArticle(Guid id)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.GetRepository<IArticleRepository>().Delete(id);

                var commentRepository = unitOfWork.GetRepository<ICommentRepository>();
                List<Guid> idsToDelete = commentRepository.GetForArticle(id)
                    .Select(x => x.Id).ToList();
                commentRepository.Delete(idsToDelete);

                unitOfWork.Commit();
            }
        }

        public void DeleteComment(Guid id)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                unitOfWork.GetRepository<ICommentRepository>().Delete(id);
                unitOfWork.Commit();
            }
        }

        public ArticleInfo GetArticleInfo(Guid id)
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                return unitOfWork.GetRepository<IArticleRepository>().Get(id)
                    .Map(x => ArticleMapper
                        .Convert(x,
                            unitOfWork
                                .GetRepository<ICommentRepository>().GetForArticle(id))).Value;
            }
        }

        public List<ArticlePreviewInfo> GetArticlePreviews()
        {
            using (var unitOfWork = unitOfWorkFactory.Create())
            {
                return unitOfWork.GetRepository<IArticleRepository>()
                    .GetAll().ConvertAll(ArticleMapper.Convert);
            }
        }
    }
}
