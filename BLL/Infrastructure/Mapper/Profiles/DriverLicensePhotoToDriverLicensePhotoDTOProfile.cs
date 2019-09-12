using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL.Infrastructure.Mapper.Profiles
{
    public class DriverLicensePhotoToDriverLicensePhotoDTOProfile : Profile
    {
        public DriverLicensePhotoToDriverLicensePhotoDTOProfile()
        {
            CreateMap<DriverLicensePhoto, DriverLicensePhotoDTO>().ReverseMap();
        }
    }
}
