﻿namespace Blog.BLL.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Changed { get; set; }

        public string UserId { get; set; }
        public UserAccountModel User { get; set; }

        public List<TagModel> Tags { get; set; }

        public List<CommentModel> Comments { get; set; }

        public List<TagModel> GetSelectedTags()
        {
            return Tags.Where(c => c.Selected).ToList();
        }
    }
}
