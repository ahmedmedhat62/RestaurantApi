/*using AutoMapper;
using RestaurantApi.Auth;
using RestaurantApi.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserDTO, User1>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FullName));
        CreateMap<User1, UserDTO>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));
    }
}*/