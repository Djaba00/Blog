using AutoMapper;
using Blog.BLL.Models;
using Blog.WebService.VIewModels.Account;
using Blog.WebService.VIewModels.Article;
using Blog.WebService.VIewModels.Comment;
using Blog.WebService.VIewModels.Tag;
using Blog.WebService.VIewModels.User;

namespace Blog.WebService.Configurations
{
    public class MappingProfilePLL : Profile
    {
        public MappingProfilePLL()
        {
            #region[User]
            CreateMap<RegistrationViewModel, UserAccountModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(vm => vm.Email));
            CreateMap<RegistrationViewModel, UserProfileModel>();

            CreateMap<LoginViewModel, UserAccountModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(vm => vm.Email));

            CreateMap<UserViewModel, UserProfileModel>()
                .ReverseMap();

            CreateMap<EditUserProfileViewModel, UserProfileModel>()
                .ReverseMap();
            #endregion

            #region[Article]
            CreateMap<ArticleViewModel, ArticleModel>()
                .ReverseMap();

            CreateMap<CreateArticleViewModel, ArticleModel>()
                .ForMember(m => m.UserId, opt => opt.MapFrom(vm => vm.AuthorId));

            CreateMap<EditArticleViewModel, ArticleModel>()
                .ReverseMap();
            #endregion

            #region[Comment]
            CreateMap<CommentViewModel, CommentModel>()
                .ReverseMap();

            CreateMap<CreateCommentViewModel, CommentModel>()
                .ReverseMap();

            CreateMap<EditCommentViewModel, CommentModel>()
                .ReverseMap();
            #endregion

            #region[Tag]
            CreateMap<TagViewModel, TagModel>()
                .ReverseMap();

            CreateMap<HashTagViewModel, TagModel>()
                .ReverseMap();

            CreateMap<CreateTagViewModel, TagModel>()
                .ReverseMap();

            CreateMap<EditTagViewModel, TagModel>()
                .ReverseMap();
            #endregion
        }
    }
}
