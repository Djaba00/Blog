using AutoMapper;
using Blog.BLL.Models;
using Blog.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface ITagService
    {
        Task CreateCommentAsync(TagModel tagModel);
        Task UpdateCommentAsync(TagModel tagModel);
        Task DeleteCommentAsync(int id);
        Task<List<TagModel>> GetAllComments();
        Task<TagModel> GetCommentById(int id);
    }
}
