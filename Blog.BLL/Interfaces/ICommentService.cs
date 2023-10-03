using Blog.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentAsync(CommentModel commentModel);
        Task UpdateCommentAsync(CommentModel commentModel);
        Task DeleteCommentAsync(int id);
        Task<List<CommentModel>> GetAllComments();
        Task<CommentModel> GetCommentById(int id);
    }
}
