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
        Task CreateArticelAsync(ArticleModel articleModel);
        Task UpdateArticleAsync(ArticleModel articleModel);
        Task DeleteArticelAsync(int id);
        Task<List<ArticleModel>> GetAllArticles();
        Task<List<ArticleModel>> GetArticlesByAuthotId(string id);
    }
}
