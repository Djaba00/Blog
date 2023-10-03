using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces
{
    public interface ITagRepository<T> : ICommonRepository<T, int>
        where T : class
    {
        Task<IEnumerable<T>> GetTagsByName(string name);
    }
}
