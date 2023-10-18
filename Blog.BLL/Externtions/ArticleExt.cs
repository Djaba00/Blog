using Blog.DAL.Entities;

namespace Blog.BLL.Externtions
{
    public static class ArticleExt
    {
        public static Article Edit(this Article article, Article updateArticle)
        {
            article.Changed = DateTime.Now;
            article.Title = updateArticle.Title;
            article.Content = updateArticle.Content;
            article.Tags = updateArticle.Tags;

            return article;
        }
    }
}
