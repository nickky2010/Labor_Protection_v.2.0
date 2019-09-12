using AutoMapper;
using BLL.DTO;
using Web.ViewModels.DriverMedicalCertificates;

namespace Web.Infrastructure.Mapper.Profiles
{
    public class DriverMedicalCertificateVModelToDriverMedicalCertificateDTOProfile : Profile
    {
        public DriverMedicalCertificateVModelToDriverMedicalCertificateDTOProfile()
        {
            CreateMap<DriverMedicalCertificateDTO, DriverMedicalCertificateAddVModel>().ReverseMap();
            CreateMap<DriverMedicalCertificateDTO, DriverMedicalCertificateGetVModel>().ReverseMap();
            CreateMap<DriverMedicalCertificateDTO, DriverMedicalCertificateUpdateVModel>().ReverseMap();
            CreateMap<DriverMedicalCertificateDTO, DriverMedicalCertificateForCategoryVModel>().ReverseMap();
            CreateMap<DriverMedicalCertificateDTO, DriverMedicalCertificateForEmployeeVModel>().ReverseMap();
        }
    }
}
