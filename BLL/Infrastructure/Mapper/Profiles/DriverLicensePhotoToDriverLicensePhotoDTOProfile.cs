using AutoMapper;
using BLL.DTO.DriverLicensePhotos;
using DAL.Models;
using System;

namespace BLL.Infrastructure.Mapper.Profiles
{
    internal class DriverLicensePhotoToDriverLicensePhotoDTOProfile : Profile
    {
        public DriverLicensePhotoToDriverLicensePhotoDTOProfile()
        {
            CreateMap<DriverLicensePhoto, DriverLicensePhotoAddDTO>();
            CreateMap<DriverLicensePhotoAddDTO, DriverLicensePhoto>()
                .AfterMap((s, d) =>
                {
                    d.Id = Guid.NewGuid();
                });
            CreateMap<DriverLicensePhoto, DriverLicensePhotoGetUpdateDTO>().ReverseMap();
        }
    }
}
