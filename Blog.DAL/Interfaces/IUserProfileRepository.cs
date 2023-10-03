using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces
{
    public interface IUserProfileRepository<T> : ICommonRepository<T, string>
        where T : class
    {

    }
}
