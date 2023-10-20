using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using System.Security.Principal;

namespace Blog.WebService.Externtions
{
    public static class UserExt
    {   
        public static UserAccount Edit(this UserAccount user, UserAccount userEdit)
        {
            user.Email = userEdit.Email;
            user.UserName = userEdit.Email;

            user.Profile.FirstName = userEdit.Profile.FirstName;
            user.Profile.LastName = userEdit.Profile.LastName;
            user.Profile.MiddleName = userEdit.Profile.MiddleName;
            user.Profile.BirthDate = userEdit.Profile.BirthDate;
            user.Profile.Image = userEdit.Profile.Image;
            user.Profile.Status = userEdit.Profile.Status;
            user.Profile.About = userEdit.Profile.About;

            user.Articles = userEdit.Articles;
            user.Comments = userEdit.Comments;

            return user;
        }

        public static async Task<UserAccount> EditRoles(this UserAccount user, UserAccountModel userEdit, IUnitOfWork db)
        {
            var updateRolesList = userEdit.Roles.Where(r => r.Selected).Select(r => r.Name).ToList();

            var userRoles = await db.UserAccounts.GetUserRolesAsync(user);

            var addedRoles = updateRolesList.Except(userRoles).ToList();

            if (addedRoles.Count > 0)
            {
                await db.UserAccounts.AddToRolesAsync(user, addedRoles);
            }

            var removedRoles = userRoles.Except(updateRolesList).ToList();

            if (removedRoles.Count > 0)
            {
                await db.UserAccounts.RemoveFromRolesAsync(user, removedRoles);
            }

            return user;
        }
    }
}
