using AutoMapper;
using BLL.DTO;
using Web.ViewModels.Positions;

namespace Web.Infrastructure.Mapper.Profiles.Positions
{
    public class PositionForEmployeeViewModelToPositionDTOProfile : Profile
    {
        public PositionForEmployeeViewModelToPositionDTOProfile()
        {
            CreateMap<PositionDTO, PositionForEmployeeViewModel>().ReverseMap();
        }
    }
}
