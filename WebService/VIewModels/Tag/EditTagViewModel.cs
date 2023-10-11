using System.ComponentModel.DataAnnotations;

namespace Blog.WebService.VIewModels.Tag
{
    public class EditTagViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
