using AutoMapper;
using BLL.DTO.DriverCategories;
using DAL.Models;
using DAL.Models.ManyToMany;
using System;

namespace BLL.Infrastructure.Mapper.Profiles
{
    internal class DriverCategoryToDriverCategoryDTOProfile : Profile
    {
        public DriverCategoryToDriverCategoryDTOProfile()
        {
            CreateMap<DriverCategory, DriverCategoryAddDTO>().ReverseMap();

            CreateMap<DriverCategory, DriverCategoryAddDTO>();
            CreateMap<DriverCategoryAddDTO, DriverCategory>()
                .AfterMap((s, d) =>
                {
                    d.Id = Guid.NewGuid();
                });

            CreateMap<DriverCategory, DriverLicenseDriverCategory>()
                .ForPath(d => d.DriverCategoryId, opt => opt.MapFrom(s => s.Id));
            CreateMap<DriverLicenseDriverCategory, DriverCategory>()
                .ForPath(d => d.Id, opt => opt.MapFrom(s => s.DriverCategoryId));
            CreateMap<DriverCategory, DriverCategoryGetUpdateDTO>().ReverseMap();
        }
    }
}
