using AutoMapper;

using IPASSData.Dtos.Authentication;
using IPASSData.Extensions;
using IPASSData.Models;
using IPASSData.Dtos.Enums;

namespace IPASSData.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Ac))
           .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.Now))
           .ReverseMap()
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Is_Active ? UserStatusEnum.啟用.GetDisplayName() : UserStatusEnum.停用.GetDisplayName()))
           .ForMember(dest => dest.UpdateTime, opt => opt.MapFrom(src => src.UpdateAt.ToDateTimeString("/")));
            //新增
            CreateMap<AddUserDto, User>()
           .ForMember(dest => dest.Is_Active, opt => opt.MapFrom(src => true))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Ac))
           .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => DateTime.Now))
           .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.Now));
            //更新
            CreateMap<UpdateUserDto, User>()
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Ac))
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.Sw, opt => opt.Ignore())
           .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<UpdatePasswordDto, User>()
           .ForMember(dest => dest.Ac, opt => opt.Ignore())
           .ForMember(dest => dest.Sw, opt => opt.MapFrom(src => src.NewSw))
           .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<User, LoginResponseDto>();
        }
    }
}
