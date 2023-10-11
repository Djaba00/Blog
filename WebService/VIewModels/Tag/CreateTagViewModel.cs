using System.ComponentModel.DataAnnotations;
using Blog.BLL.Models;
using Blog.WebService.VIewModels.Article;

namespace Blog.WebService.VIewModels.Tag
{
    public class CreateTagViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
