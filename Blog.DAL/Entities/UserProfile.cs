﻿using System;
using Microsoft.AspNetCore.Identity;

namespace Blog.DAL.Entities
{
	public class UserProfile
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string About { get; set; }

		public string UserAccountId { get; set; }
		public virtual UserAccount UserAccount { get; set; }
    }
}

