using System.ComponentModel.DataAnnotations;
using Blog.WebService.ViewModels.Tag;

namespace Blog.WebService.ViewModels.Article
{
    public class CreateArticleViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 50)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public List<HashTagViewModel> Tags { get; set; } = new List<HashTagViewModel>();

        public List<HashTagViewModel> GetSelectedTags()
        {
            return Tags.Where(c => c.Selected).ToList();
        }
    }
}