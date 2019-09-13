using AutoMapper;
using BLL.DTO.Positions;
using DAL.Models;

namespace BLL.Infrastructure.Mapper.Profiles
{
    internal class PositionToPositionDTOProfile : Profile
    {
        public PositionToPositionDTOProfile()
        {
            CreateMap<Position, PositionAddDTO>().ReverseMap();
            CreateMap<Position, PositionGetUpdateDTO>().ReverseMap();
        }
    }
}
