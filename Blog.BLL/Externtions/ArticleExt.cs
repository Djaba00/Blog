using AutoMapper;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using System.Linq;

namespace Blog.BLL.Externtions
{
    public static class ArticleExt
    {
        public static Article Create(this Article newArticle)
        {
            newArticle.Created = DateTime.Now;

            return newArticle;
        }
        
        public static async Task<Article> Edit(this Article article, Article articleEdit)
        {
            article.Changed = DateTime.Now;
            article.Title = articleEdit.Title;
            article.Content = articleEdit.Content;

            await EditTags(article, articleEdit);

            return article;
        }

        private static async Task EditTags(Article article, Article articleEdit)
        {
            var updateTags = articleEdit.Tags.ToList();

            var articleTags = article.Tags.ToList();

            var addedTags = updateTags.ExceptBy(articleTags.Select(p => p.Name), p => p.Name).ToList();

            if (addedTags.Count > 0)
            {
                foreach ( var tag in addedTags)
                {
                    article.Tags.Add(tag);
                }
            }

            var removedTags = articleTags.ExceptBy(updateTags.Select(p => p.Name), p => p.Name).ToList();

            if (removedTags.Count > 0)
            {
                foreach (var tag in removedTags)
                {
                    article.Tags.Remove(tag);
                }
            }
        }
    }
}
