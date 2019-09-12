using AutoMapper;
using BLL.DTO;
using System.Collections.Generic;
using Web.ViewModels.Positions;

namespace Web.Infrastructure.Mapper.Profiles
{
    public class PositionVModelToPositionDTOProfile : Profile
    {
        public PositionVModelToPositionDTOProfile()
        {
            CreateMap<PositionDTO, PositionAddVModel>().ReverseMap();
            CreateMap<PositionDTO, PositionGetVModel>().ReverseMap();
            CreateMap<PositionDTO, PositionUpdateVModel>().ReverseMap();
        }
    }
}
