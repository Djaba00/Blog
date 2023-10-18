using Blog.DAL.Entities;

namespace Blog.BLL.Externtions
{
    public static class CommentExt
    {
        public static Comment Edit(this Comment comment, Comment updateComment)
        {
            comment.Changed = DateTime.Now;
            comment.Content = updateComment.Content;

            return comment;
        }
    }
}
