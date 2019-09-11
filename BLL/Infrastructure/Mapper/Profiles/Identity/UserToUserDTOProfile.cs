using AutoMapper;
using BLL.DTO.Identity;
using DAL.Models.Identity;

namespace BLL.Infrastructure.Mapper.Profiles.Identity
{
    public class UserToUserDTOProfile : Profile
    {
        public UserToUserDTOProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
