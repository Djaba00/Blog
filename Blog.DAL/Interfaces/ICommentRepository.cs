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
        
    }
}
