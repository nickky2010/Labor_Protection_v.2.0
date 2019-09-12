using AutoMapper;
using BLL.DTO;
using Web.ViewModels.DriverMedicalCertificatesPhotos;

namespace Web.Infrastructure.Mapper.Profiles
{
    public class DriverMedicalCertificatePhotoVModelToDriverMedicalCertificatePhotoDTOProfile : Profile
    {
        public DriverMedicalCertificatePhotoVModelToDriverMedicalCertificatePhotoDTOProfile()
        {
            CreateMap<DriverMedicalCertificatePhotoDTO, DriverMedicalCertificatePhotoAddVModel>().ReverseMap();
            CreateMap<DriverMedicalCertificatePhotoDTO, DriverMedicalCertificatePhotoDMCAddVModel>().ReverseMap();
            CreateMap<DriverMedicalCertificatePhotoDTO, DriverMedicalCertificatePhotoGetVModel>().ReverseMap();
            CreateMap<DriverMedicalCertificatePhotoDTO, DriverMedicalCertificatePhotoUpdateVModel>().ReverseMap();
        }
    }
}
