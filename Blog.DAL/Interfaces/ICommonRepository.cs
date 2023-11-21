
using Blog.DAL.Entities;

namespace Blog.DAL.Interfaces
{
    public interface ICommonRepository<T1, T2>
        where T1 : class
    {
        Task<T1> FindAsync(T2 id);
        Task<IEnumerable<T1>> GetAllAsync();
        Task<T1> GetByIdAsync(T2 id);
        void Create(T1 entity);
        void Update(T1 entity);
        void Delete(T1 entity);
    }
}
