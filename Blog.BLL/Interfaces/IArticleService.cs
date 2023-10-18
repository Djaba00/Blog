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
        Task CreateArticleAsync(ArticleModel articleModel);
        Task UpdateArticleAsync(ArticleModel articleModel);
        Task DeleteArticleAsync(int id);
        Task<List<ArticleModel>> GetAllArticlesAsync();
        Task<ArticleModel> GetArticleByIdAsync(int id);
        Task<List<ArticleModel>> GetArticlesByAuthorIdAsync(int id);
        Task<List<ArticleModel>> GetArticlesByTitleAsync(string title);
    }
}
