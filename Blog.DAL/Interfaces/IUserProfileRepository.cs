using Blog.DAL.Entities;

namespace Blog.DAL.Interfaces
{
    public interface IUserProfileRepository<T> : ICommonRepository<T, int>
        where T : class
    {
        Task<UserProfile> FindByIdAsync(int id);
        Task<UserProfile> GetByAccountId(string id);
    }
}
