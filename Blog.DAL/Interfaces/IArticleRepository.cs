using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces
{
    public interface IArticleRepository<T> : ICommonRepository<T, int>
        where T : class
    {
        Task<List<T>> GetArticlesByName(string name);
        Task<List<Article>> GetArticlesByAuthorId(string id);
    }
}
