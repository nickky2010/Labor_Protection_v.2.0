using AutoMapper;
using BLL.DTO;
using Web.ViewModels.DriverMedicalCertificates;

namespace Web.Infrastructure.Mapper.Profiles.DriverMedicalCertificates
{
    public class DriverMedicalCertificateForEmployeeViewModelToDriverMedicalCertificateDTOProfile : Profile
    {
        public DriverMedicalCertificateForEmployeeViewModelToDriverMedicalCertificateDTOProfile()
        {
            CreateMap<DriverMedicalCertificateDTO, DriverMedicalCertificateForEmployeeViewModel>().ReverseMap();
        }
    }
}
