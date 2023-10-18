using Blog.BLL.Models;
using Blog.DAL.Entities;

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

            user.Profile.Articles = userEdit.Profile.Articles;
            user.Profile.Comments = userEdit.Profile.Comments;

            return user;
        }
    }
}
