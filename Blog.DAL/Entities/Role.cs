using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Entities
{
    public class Role : IdentityRole
    {
        public virtual ICollection<UserAccount> Accounts { get; set; }
    }
}
