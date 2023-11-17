using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.Requests.Tag
{
	public class EditTagRequest
	{
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

