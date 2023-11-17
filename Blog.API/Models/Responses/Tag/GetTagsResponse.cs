using Blog.API.Models.ViewModels;
using System;

namespace Blog.API.Models.Responses.Tag
{
	public class GetTagsResponse
	{
		public List<TagViewModel> Tags { get; set; }
	}
}

