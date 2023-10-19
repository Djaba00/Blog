using Blog.DAL.Entities;

namespace Blog.DAL.Interfaces
{
    public interface IArticleRepository<T> : ICommonRepository<T, int>
        where T : class
    {
        Task<Article> FindArticleAsync(Article entity);
        Task<List<T>> GetArticlesByTitleAsync(string title);
        Task<List<Article>> GetArticlesByAuthorIdAsync(int authorId);
        Task<List<Article>> GetArticlesByTagAsync(string tag);
    }
}
