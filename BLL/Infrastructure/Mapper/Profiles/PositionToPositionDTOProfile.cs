using AutoMapper;
using BLL.DTO.Positions;
using DAL.Models;
using System;

namespace BLL.Infrastructure.Mapper.Profiles
{
    internal class PositionToPositionDTOProfile : Profile
    {
        public PositionToPositionDTOProfile()
        {
            CreateMap<Position, PositionAddDTO>();
            CreateMap<PositionAddDTO, Position>()
                .AfterMap((s, d) =>
                {
                    d.Id = Guid.NewGuid();
                });
            CreateMap<Position, PositionGetUpdateDTO>().ReverseMap();
        }
    }
}
