﻿using AutoMapper;
using BLL.DTO.DriverLicensePhotos;
using DAL.Models;
using System;
using System.IO;

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
                    using (var reader = new BinaryReader(s.Picture.OpenReadStream()))
                    {
                        d.Photo = reader.ReadBytes((int)s.Picture.Length);
                    }
                });
            CreateMap<DriverLicensePhoto, DriverLicensePhotoGetDTO>()
                .ForPath(d => d.Picture, opt => opt.MapFrom(s => s.Photo));
            CreateMap<DriverLicensePhotoGetDTO, DriverLicensePhoto>()
                .ForPath(d => d.Photo, opt => opt.MapFrom(s => s.Picture));

            CreateMap<DriverLicensePhoto, DriverLicensePhotoUpdateDTO>();
            CreateMap<DriverLicensePhotoUpdateDTO, DriverLicensePhoto>()
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
