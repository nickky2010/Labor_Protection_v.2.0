using AutoMapper;
using BLL.DTO.Identity;
using DAL.Models.Identity;

namespace BLL.Infrastructure.Mapper.Profiles.Identity
{
    public class UserProfileToUserProfileDTOProfile : Profile
    {
        public UserProfileToUserProfileDTOProfile()
        {
            CreateMap<UserProfile, UserProfileDTO>().ReverseMap();
        }
    }
}
