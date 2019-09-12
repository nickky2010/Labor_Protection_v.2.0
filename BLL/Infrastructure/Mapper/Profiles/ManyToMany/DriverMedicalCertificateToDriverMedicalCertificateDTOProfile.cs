using AutoMapper;
using BLL.DTO;
using DAL.Models;
using DAL.Models.ManyToMany;

namespace BLL.Infrastructure.Mapper.Profiles.ManyToMany
{
    public class DriverMedicalCertificateToDriverMedicalCertificateDTOProfile : Profile
    {
        public DriverMedicalCertificateToDriverMedicalCertificateDTOProfile()
        {
            CreateMap<DriverMedicalCertificate, DriverMedicalCertificateDTO>();

            CreateMap<DriverMedicalCertificateDriverCategory, DriverCategoryDTO>()
                    .ForMember(d => d.Id, opt => opt.MapFrom(s => s.DriverCategoryId))
                    .ForMember(d => d.DriverMedicalCertificates, opt => opt.MapFrom(s => s.DriverCategory.Id));

            CreateMap<DriverMedicalCertificateDTO, DriverMedicalCertificate>()
                  .AfterMap((s, d) =>
                  {
                      foreach (var stud in d.DriverMedicalCertificateDriverCategories)
                          stud.DriverMedicalCertificateId = s.Id;
                  });

            CreateMap<DriverCategoryDTO, DriverMedicalCertificateDriverCategory>()
                  .ForMember(d => d.DriverCategoryId, opt => opt.MapFrom(s => s.Id));
        }
    }
}
