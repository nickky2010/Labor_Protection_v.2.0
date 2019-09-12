using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL.Infrastructure.Mapper.Profiles
{
    public class DriverCategoryToDriverCategoryDTOProfile : Profile
    {
        public DriverCategoryToDriverCategoryDTOProfile()
        {
            CreateMap<DriverCategory, DriverCategoryDTO>().ReverseMap();
        }
    }
}
