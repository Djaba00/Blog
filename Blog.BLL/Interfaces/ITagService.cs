using Blog.BLL.Models;

namespace Blog.BLL.Interfaces
{
    public interface ITagService
    {
        Task CreateTagAsync(TagModel tagModel);
        Task UpdateTagAsync(TagModel tagModel);
        Task DeleteTagAsync(int id);
        Task<List<TagModel>> GetAllTagsAsync();
        Task<TagModel> GetTagByIdAsync(int id);
        Task<TagModel> GetTagByNameAsync(string name);
    }
}
