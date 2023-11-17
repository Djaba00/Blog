using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.API.Models.Requests.Tag
{
	public class CreateTagRequest
	{
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

