using Blog.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IArticleService
    {
        Task<List<ArticleModel>> GetAllArticlesAsync();
        Task<ArticleModel> GetArticleByIdAsync(int id);
        Task<List<ArticleModel>> GetArticlesByAuthorIdAsync(string authorId);
        Task<List<ArticleModel>> GetArticlesByTagAsync(string tagName);
        Task<List<ArticleModel>> GetArticlesByTitleAsync(string title);

        Task<bool> CanChangeArticleAsync(ClaimsPrincipal claims, ArticleModel article);

        Task<ArticleModel> GetUpdateArticle(ClaimsPrincipal claims, int id);
        Task CreateArticleAsync(ArticleModel articleModel);
        Task UpdateArticleAsync(ClaimsPrincipal claims, ArticleModel articleModel);
        Task DeleteArticleAsync(ClaimsPrincipal claims, int id);
    }
}
