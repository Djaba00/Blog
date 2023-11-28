using System;
using Blog.API.Models.ViewModels;

namespace Blog.API.Models.Responses.Comment
{
	public class GetCommentsResponse
	{
		public List<CommentViewModel> Coments { get; set; }
	}
}

