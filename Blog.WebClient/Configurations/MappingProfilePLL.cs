using AutoMapper;
using Blog.BLL.Models;
using Blog.WebClient.VIewModels.Account;
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

            CreateMap<EditUserViewModel, UserAccountModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(vm => vm.Email))
                .ReverseMap();
        }
    }
}
