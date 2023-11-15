using System.ComponentModel.DataAnnotations;
using Blog.DAL.Entities;
using Blog.WebService.ViewModels.Account;
using Blog.WebService.ViewModels.Tag;

namespace Blog.WebService.ViewModels.Article
{
    public class EditArticleViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, MinimumLength = 50)]
        public string Content { get; set; }

        public List<HashTagViewModel> Tags { get; set; }

        public string UserId { get; set; }

        public AccountViewModel CurrentUser { get; set; } = new AccountViewModel();

        public List<HashTagViewModel> GetSelectedTags()
        {
            return Tags.Where(c => c.Selected).ToList();
        }
    }  
}
