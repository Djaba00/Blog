using AutoMapper;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Configurations
{
    public class MappingProfileBLL : Profile
    {
        public MappingProfileBLL()
        {
            CreateMap<UserAccountModel, UserAccount>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(x => x.Email))
                .ReverseMap();

            CreateMap<RoleModel, Role>()
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
