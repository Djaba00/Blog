using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces
{
    public interface ICommentRepository<T> : ICommonRepository<T, int> 
        where T : class
    {
        Task<List<Comment>> GetCommentsByAuthorIdAsync(string id);
        Task<List<Comment>> GetCommentsByArticleIdAsync(int id);
    }
}
