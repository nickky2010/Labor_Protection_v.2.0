﻿using BLL.Interfaces;
using DAL.Extentions;
using Microsoft.AspNetCore.Http;
using System;

namespace BLL.DTO.DriverLicensePhotos
{
    public class DriverLicensePhotoAddDTO : AbstractDataDTO<DriverLicensePhotoAddDTO>, IAddDTO, IAddUpdatePhotoDTO
    {
        public IFormFile Picture { get; set; }
        public Guid DriverLicenseId { get; set; }

        public override int GetHashCode()
        {
            int hash = 17;
            return hash ^= 31 + DriverLicenseId.ToString().ToInt();
        }
    }
}