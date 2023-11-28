using AutoMapper;
using Blog.API.Models.Requests.Account;
using Blog.API.Models.Requests.Article;
using Blog.API.Models.Requests.Comment;
using Blog.API.Models.Requests.Role;
using Blog.API.Models.Requests.SignIn;
using Blog.API.Models.Requests.Tag;
using Blog.API.Models.Responses.Account;
using Blog.API.Models.Responses.AccountRole;
using Blog.API.Models.Responses.Article;
using Blog.API.Models.Responses.Tag;
using Blog.API.Models.ViewModels;
using Blog.BLL.Models;

namespace Blog.API.Configurations
{
	public class MappingProfileAPI : Profile
	{
		public MappingProfileAPI()
		{
            #region[User]

            CreateMap<UserProfileViewModel, UserProfileModel>()
                .ReverseMap();

            CreateMap<AccountViewModel, UserAccountModel>()
                .ReverseMap();

            CreateMap<RegistrationRequest, UserAccountModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(vm => vm.Email));
            CreateMap<RegistrationRequest, UserProfileModel>();

            CreateMap<LoginRequest, UserAccountModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(vm => vm.Email));

            CreateMap<EditAccountRequest, UserAccountModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(vm => vm.Email))
                .ReverseMap();

            CreateMap<GetAccountResponse, UserAccountModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(vm => vm.Email))
                .ReverseMap();

            #endregion

            #region[Role]

            CreateMap<RoleViewModel, AccountRoleModel>()
                .ReverseMap();

            CreateMap<CreateRoleRequest, AccountRoleModel>()
                .ReverseMap();

            CreateMap<EditRoleRequest, AccountRoleModel>()
                .ReverseMap();

            #endregion

            #region[Article]

            CreateMap<ArticleViewModel, ArticleModel>()
                .ReverseMap();

            CreateMap<CreateArticleRequest, ArticleModel>()
                .ForMember(m => m.UserId, opt => opt.MapFrom(vm => vm.UserId))
                .ForMember(m => m.Tags, opt => opt.MapFrom(vm => vm.Tags));

            CreateMap<EditArticleRequest, ArticleModel>()
                .ReverseMap();

            CreateMap<GetArticleResponse, ArticleModel>()
                .ReverseMap();

            #endregion

            #region[Comment]

            CreateMap<CommentViewModel, CommentModel>()
                .ReverseMap();

            CreateMap<CreateCommentRequest, CommentModel>()
                .ReverseMap();

            CreateMap<EditCommentRequest, CommentModel>()
                .ReverseMap();

            #endregion

            #region[Tag]

            CreateMap<TagViewModel, TagModel>()
                .ReverseMap();

            CreateMap<HashTagViewModel, TagModel>()
                .ReverseMap();

            CreateMap<CreateTagRequest, TagModel>()
                .ReverseMap();

            CreateMap<EditTagRequest, TagModel>()
                .ReverseMap();

            #endregion
        }
    }
}

