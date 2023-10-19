using System.ComponentModel.DataAnnotations;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.Article;

namespace Blog.WebService.ViewModels.Tag
{
    public class CreateTagViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
