using AutoMapper;
using BLL.DTO;
using Web.ViewModels.DriverLicensePhotos;

namespace Web.Infrastructure.Mapper.Profiles
{
    public class DriverLicensePhotoVModelToDriverLicensePhotoDTOProfile : Profile
    {
        public DriverLicensePhotoVModelToDriverLicensePhotoDTOProfile()
        {
            CreateMap<DriverLicensePhotoDTO, DriverLicensePhotoAddVModel>().ReverseMap();
            CreateMap<DriverLicensePhotoDTO, DriverLicensePhotoForDLAddVModel>().ReverseMap();
            CreateMap<DriverLicensePhotoDTO, DriverLicensePhotoGetVModel>().ReverseMap();
            CreateMap<DriverLicensePhotoDTO, DriverLicensePhotoUpdateVModel>().ReverseMap();
        }
    }
}
