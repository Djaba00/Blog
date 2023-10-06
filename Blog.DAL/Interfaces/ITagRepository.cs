using System;

namespace Blog.DAL.Interfaces
{
    public interface ITagRepository<T> : ICommonRepository<T, int>
        where T : class
    {
        Task<IEnumerable<T>> GetTagsByName(string name);
    }
}
