﻿using System;

namespace Blog.DAL.Entities
{
	public class Tag
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<Article> Articles { get; set; }
	}
}

