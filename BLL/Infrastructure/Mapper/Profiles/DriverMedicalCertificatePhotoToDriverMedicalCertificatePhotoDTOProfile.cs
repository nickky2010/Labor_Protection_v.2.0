using AutoMapper;
using BLL.DTO.DriverMedicalCertificatePhotos;
using DAL.Models;
using System;
using System.IO;

namespace BLL.Infrastructure.Mapper.Profiles
{
    internal class DriverMedicalCertificatePhotoToDriverMedicalCertificatePhotoDTOProfile : Profile
    {
        public DriverMedicalCertificatePhotoToDriverMedicalCertificatePhotoDTOProfile()
        {
            CreateMap<DriverMedicalCertificatePhoto, DriverMedicalCertificatePhotoAddDTO>();
            CreateMap<DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhoto>()
                .AfterMap((s, d) =>
                {
                    d.Id = Guid.NewGuid();
                    using (var reader = new BinaryReader(s.Picture.OpenReadStream()))
                    {
                        d.Photo = reader.ReadBytes((int)s.Picture.Length);
                    }
                });
            CreateMap<DriverMedicalCertificatePhoto, DriverMedicalCertificatePhotoGetDTO>()
                .ForPath(d => d.Picture, opt => opt.MapFrom(s => s.Photo));
            CreateMap<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhoto>()
                .ForPath(d => d.Photo, opt => opt.MapFrom(s => s.Picture));

            CreateMap<DriverMedicalCertificatePhoto, DriverMedicalCertificatePhotoUpdateDTO>();
            CreateMap<DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto>()
                .AfterMap((s, d) =>
                {
                    using (var reader = new BinaryReader(s.Picture.OpenReadStream()))
                    {
                        d.Photo = reader.ReadBytes((int)s.Picture.Length);
                    }
                });
        }
    }
}
