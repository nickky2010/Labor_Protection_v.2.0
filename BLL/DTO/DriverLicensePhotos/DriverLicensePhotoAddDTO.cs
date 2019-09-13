﻿using BLL.Interfaces;
using DAL.Extentions;

namespace BLL.DTO.DriverLicensePhotos
{
    public class DriverLicensePhotoAddDTO : AbstractDataDTO<DriverLicensePhotoAddDTO>, IAddDTO
    {
        public byte[] Picture { get; set; }
        public override int GetHashCode()
        {
            int hash = 17;
            return hash ^= 31 + Picture.ToString().ToInt();
        }
    }
}