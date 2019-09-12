using AutoMapper;
using BLL.DTO;
using Web.ViewModels.DriverCategories;

namespace Web.Infrastructure.Mapper.Profiles
{
    public class DriverCategoryVModelToPositionDTOProfile : Profile
    {
        public DriverCategoryVModelToPositionDTOProfile()
        {
            CreateMap<PositionDTO, DriverCategoryAddVModel>().ReverseMap();
            CreateMap<PositionDTO, DriverCategoryForModelsVModel>().ReverseMap();
            CreateMap<PositionDTO, DriverCategoryGetVModel>().ReverseMap();
            CreateMap<PositionDTO, DriverCategoryUpdateVModel>().ReverseMap();
        }
    }
}
