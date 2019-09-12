using AutoMapper;
using BLL.DTO;
using DAL.Models;
using DAL.Models.ManyToMany;

namespace BLL.Infrastructure.Mapper.Profiles.ManyToMany
{
    public class DriverLicenseToDriverLicenseDTOProfile : Profile
    {
        public DriverLicenseToDriverLicenseDTOProfile()
        {
            CreateMap<DriverLicense, DriverLicenseDTO>();

            CreateMap<DriverLicenseDriverCategory, DriverCategoryDTO>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.DriverCategoryId))
                    .ForMember(d => d.DriverLicenses, opt => opt.MapFrom(s => s.DriverCategory.Id));

            CreateMap<DriverLicenseDTO, DriverLicense>()
                  .AfterMap((s, d) =>
                  {
                      foreach (var stud in d.DriverLicenseDriverCategories)
                          stud.DriverLicenseId = s.Id;
                  });

            CreateMap<DriverCategoryDTO, DriverMedicalCertificateDriverCategory>()
                  .ForMember(d => d.DriverCategoryId, opt => opt.MapFrom(s => s.Id));

            CreateMap<DriverCategory, DriverMedicalCertificateDriverCategory>()
                .ForMember(entity => entity.DriverCategory, opt => opt.MapFrom(model => model));
            CreateMap<DriverMedicalCertificateDriverCategory, DriverCategory>()
                .ForMember(entity => entity.DriverMedicalCertificateDriverCategories, opt => opt.MapFrom(model => model));

        }
    }
}
