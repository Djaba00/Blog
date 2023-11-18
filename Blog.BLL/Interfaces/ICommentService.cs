using Blog.BLL.Models;
using System.Security.Claims;

namespace Blog.BLL.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentModel>> GetAllCommentsAsync();
        Task<CommentModel> GetCommentByIdAsync(int id);
        Task<List<CommentModel>> GetCommentsByAuthorIdAsync(string id);
        Task<List<CommentModel>> GetCommentsByArticleIdAsync(int id);

        Task<bool> CanChangeCommentAsync(ClaimsPrincipal claims, CommentModel commentModel);
        Task<CommentModel> GetUpdateCommentAsync(ClaimsPrincipal claims, int id);

        Task CreateCommentAsync(CommentModel commentModel);
        Task UpdateCommentAsync(ClaimsPrincipal claims, CommentModel commentModel);
        Task DeleteCommentAsync(ClaimsPrincipal claims, int id);
    }
}
