using Blog.DAL.Entities;

namespace Blog.DAL.Interfaces
{
    public interface ICommentRepository<T> : ICommonRepository<T, int> 
        where T : class
    {
        Task<List<Comment>> GetCommentsByAuthorIdAsync(string id);
        Task<List<Comment>> GetCommentsByArticleIdAsync(int id);
    }
}
