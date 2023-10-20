namespace Blog.WebService.ViewModels.Comment
{
    public class EditCommentViewModel
    {
        public int Id {  get; set; }
        public string Content { get; set; }

        public string UserId { get; set; }

        public string ArticleId { get; set; }
    }
}
