using AutoMapper;
using Blog.BLL.Models;
using Blog.DAL.Entities;

namespace Blog.BLL.Configurations
{
    public class MappingProfileBLL : Profile
    {
        public MappingProfileBLL()
        {
            CreateMap<UserAccountModel, UserAccount>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(x => x.Email))
                .ReverseMap();

            CreateMap<AccountRoleModel, Role>()
                .ReverseMap();

            CreateMap<UserProfileModel, UserProfile>()
                .ReverseMap();

            CreateMap<ArticleModel, Article>()
                .ReverseMap();

            CreateMap<CommentModel, Comment>()
                .ReverseMap();

            CreateMap<TagModel, Tag>()
                .ReverseMap();
        }
    }
}
