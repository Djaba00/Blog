using System;
namespace Blog.API.Models.Requests.Role
{
	public class CreateRoleRequest
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

