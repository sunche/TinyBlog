using System;
using Tinyblog.Client.Common;
using Tinyblog.Contracts.Data;

namespace Tinyblog.Client.ViewModels
{
    public class CommentViewModel : ViewModelBase
    {
        private readonly CommentInfo commentInfo;

        [Obsolete]
        public CommentViewModel()
        {
        }

        public CommentViewModel(CommentInfo commentInfo, Action<object> deleteComment)
        {
            this.commentInfo = commentInfo;
            DeleteCommentCommand = new Command(CanDeleteComment, deleteComment);
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Author => commentInfo.Author;

        /// <summary>
        /// Gets or sets the delete comment command.
        /// </summary>
        public Command DeleteCommentCommand { get; }

        /// <summary>
        /// Gets the comment identifier.
        /// </summary>
        public Guid Id => commentInfo.Id;

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text => commentInfo.Text;

        private bool CanDeleteComment(object obj)
        {
            return commentInfo.Author == CurrentUser;
        }
    }
}
