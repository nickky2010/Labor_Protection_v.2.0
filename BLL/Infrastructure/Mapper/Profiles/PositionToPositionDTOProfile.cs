using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL.Infrastructure.Mapper.Profiles
{
    public class PositionToPositionDTOProfile : Profile
    {
        public PositionToPositionDTOProfile()
        {
            CreateMap<Position, PositionDTO>().ReverseMap();
        }
    }
}
