using Blog.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IArticleService
    {
        Task<List<ArticleModel>> GetAllArticlesAsync();
        Task<ArticleModel> GetArticleByIdAsync(int id);
        Task<ArticleModel> GetArticleByIdForEditAsync(int id);
        Task<List<ArticleModel>> GetArticlesByAuthorIdAsync(string authorId);
        Task<List<ArticleModel>> GetArticlesByTagAsync(string tagName);
        Task<List<ArticleModel>> GetArticlesByTitleAsync(string title);

        Task CreateArticleAsync(ArticleModel articleModel);
        Task UpdateArticleAsync(ArticleModel articleModel);
        Task DeleteArticleAsync(int id);
    }
}
