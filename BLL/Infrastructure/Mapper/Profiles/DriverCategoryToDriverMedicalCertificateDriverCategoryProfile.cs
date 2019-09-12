using AutoMapper;
using BLL.DTO;
using DAL.Models;
using DAL.Models.ManyToMany;

namespace BLL.Infrastructure.Mapper.Profiles
{
    public class DriverCategoryToDriverMedicalCertificateDriverCategoryProfile : Profile
    {
        public DriverCategoryToDriverMedicalCertificateDriverCategoryProfile()
        {
            CreateMap<DriverCategory, DriverMedicalCertificateDriverCategory>()
                .ForMember(entity => entity.DriverCategory, opt => opt.MapFrom(model => model));
            CreateMap<DriverMedicalCertificateDriverCategory, DriverCategory>()
                .ForMember(entity => entity.DriverMedicalCertificateDriverCategories, opt => opt.MapFrom(model => model));
        }
    }
}
