using AutoMapper;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.Account;
using Blog.WebService.ViewModels.AccountRole;
using Blog.WebService.ViewModels.Article;
using Blog.WebService.ViewModels.Comment;
using Blog.WebService.ViewModels.Tag;
using Blog.WebService.ViewModels.UserProfile;

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

            CreateMap<UserProfileViewModel, UserProfileModel>()
                .ReverseMap();

            CreateMap<UserProfileViewModel, UserProfileModel>()
                .ReverseMap();

            CreateMap<AccountViewModel, UserAccountModel>()
                .ReverseMap();

            CreateMap<EditAccountViewModel, UserAccountModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(vm => vm.Email))
                .ReverseMap();
            #endregion

            #region[Role]

            CreateMap<CreateAccountRoleViewModel, AccountRoleModel>()
                .ReverseMap();

            CreateMap<EditAccountRoleViewModel, AccountRoleModel>()
                .ReverseMap();

            CreateMap<AccountRoleViewModel, AccountRoleModel>()
                .ReverseMap();
            
            #endregion

            #region[Article]
            CreateMap<ArticleViewModel, ArticleModel>()
                .ReverseMap();

            CreateMap<CreateArticleViewModel, ArticleModel>()
                .ForMember(m => m.UserId, opt => opt.MapFrom(vm => vm.AuthorId))
                .ForMember(m => m.Tags, opt => opt.MapFrom(vm => vm.ArticleTags));

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
