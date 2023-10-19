using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Externtions
{
    public static class RoleExt
    {
        public static Role Edit(this Role role, Role updateRole)
        {
            role.Name = updateRole.Name;
            role.Description = updateRole.Description;

            return role;
        }
    }
}
