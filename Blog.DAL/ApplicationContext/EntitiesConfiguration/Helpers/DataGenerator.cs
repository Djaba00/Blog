using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.ApplicationContext.EntitiesConfiguration.Helpers
{
    public static class DataGenerator
    {
        public static List<Role> roles = new List<Role>();
        public static List<UserAccount> accounts = new List<UserAccount>();

        static DataGenerator()
        {
            Generate();
        }

        static void Generate()
        {
            var role1 = new Role() { Id = "1", Name = "Admin", NormalizedName = "ADMIN" };
            var role2 = new Role() { Id = "2", Name = "Moder", NormalizedName = "MODER" };
            var role3 = new Role() { Id = "3", Name = "User", NormalizedName = "USER" };

            roles.Add(role1);
            roles.Add(role2);
            roles.Add(role3);

            var user1 = new UserAccount() { Id = "1", UserName = "AdminTest", NormalizedUserName = "ADMINTEST", Email = "admin@gmail.com", NormalizedEmail = "ADMIN@GMAIL.COM", RoleId = role1.Id, Role = role1 };
            var user2 = new UserAccount() { Id = "2", UserName = "ModerTest", NormalizedUserName = "MODERTEST", Email = "moder@gmail.com", NormalizedEmail = "MODER@GMAIL.COM", RoleId = role2.Id, Role = role2 };
            var user3 = new UserAccount() { Id = "3", UserName = "UserTest", NormalizedUserName = "USERTEST", Email = "user@gmail.com", NormalizedEmail = "USER@GMAIL.COM", RoleId = role3.Id, Role = role3 };

            accounts.Add(user1);
            accounts.Add(user2);
            accounts.Add(user3);
        }
    }
}
