using System.ComponentModel.DataAnnotations;

namespace Blog.WebService.ViewModels.Tag
{
    public class EditTagViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}