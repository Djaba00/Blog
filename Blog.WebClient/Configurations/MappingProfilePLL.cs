using AutoMapper;
using Blog.BLL.Models;
using Blog.WebClient.VIewModels.Account;
using Blog.WebClient.VIewModels.Article;
using Blog.WebClient.VIewModels.Comment;
using Blog.WebClient.VIewModels.Tag;
using Blog.WebClient.VIewModels.User;

namespace Blog.WebClient.Configurations
{
    public class MappingProfilePLL : Profile
    {
        public MappingProfilePLL()
        {
            CreateMap<RegistrationViewModel, UserAccountModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(vm => vm.Email));

            CreateMap<LoginViewModel, UserAccountModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(vm => vm.Email));

            CreateMap<UserViewModel, UserAccountModel>()
                .ReverseMap();

            CreateMap<EditUserProfileViewModel, UserProfileModel>()
                .ReverseMap();

            CreateMap<ArticleViewModel, ArticleModel>()
                .ReverseMap();

            CreateMap<CreateArticleViewModel, ArticleModel>()
                .ReverseMap();

            CreateMap<EditArticleViewModel, ArticleModel>()
                .ReverseMap();

            CreateMap<CommentViewModel, CommentModel>()
                .ReverseMap();

            CreateMap<CreateCommentViewModel, CommentModel>()
                .ReverseMap();

            CreateMap<EditCommentViewModel, CommentModel>()
                .ReverseMap();

            CreateMap<TagViewModel, TagModel>()
                .ReverseMap();

            CreateMap<CreateTagViewModel, TagModel>()
                .ReverseMap();

            CreateMap<EditTagViewModel, TagModel>()
                .ReverseMap();
        }
    }
}
