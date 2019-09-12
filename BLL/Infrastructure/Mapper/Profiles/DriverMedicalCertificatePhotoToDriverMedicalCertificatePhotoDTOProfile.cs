using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL.Infrastructure.Mapper.Profiles
{
    public class DriverMedicalCertificatePhotoToDriverMedicalCertificatePhotoDTOProfile : Profile
    {
        public DriverMedicalCertificatePhotoToDriverMedicalCertificatePhotoDTOProfile()
        {
            CreateMap<DriverMedicalCertificatePhoto, DriverMedicalCertificatePhotoDTO>().ReverseMap();
        }
    }
}
