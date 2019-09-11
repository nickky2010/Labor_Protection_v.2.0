using AutoMapper;
using BLL.DTO.Identity;
using DAL.Models.Identity;

namespace BLL.Infrastructure.Mapper.Profiles.Identity
{
    public class RoleToRoleDTOProfile : Profile
    {
        public RoleToRoleDTOProfile()
        {
            CreateMap<Role, RoleDTO>().ReverseMap();
        }
    }
}
