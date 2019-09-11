using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL.Infrastructure.Mapper.Profiles
{
    public class DriverMedicalCertificateToDriverMedicalCertificateDTOProfile : Profile
    {
        public DriverMedicalCertificateToDriverMedicalCertificateDTOProfile()
        {
            CreateMap<DriverMedicalCertificate, DriverMedicalCertificateDTO>().ReverseMap();
        }
    }
}
