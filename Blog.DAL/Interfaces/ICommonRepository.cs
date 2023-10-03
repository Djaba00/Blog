using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces
{
    public interface ICommonRepository<T1, T2>
        where T1 : class
    {
        Task<IEnumerable<T1>> GetAllAsync();
        Task<T1> GetByIdAsync(T2 id);
        Task CreateAsync(T1 entity);
        void Update(T1 entity);
        void Delete(int id);
    }
}
