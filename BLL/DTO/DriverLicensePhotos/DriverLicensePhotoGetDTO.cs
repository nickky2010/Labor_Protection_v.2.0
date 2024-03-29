﻿using BLL.Interfaces;
using DAL.Extentions;
using System;

namespace BLL.DTO.DriverLicensePhotos
{
    public class DriverLicensePhotoGetDTO : AbstractDataDTO<DriverLicensePhotoGetDTO>, IUpdateDTO, IGetDTO, IGetPhotoDTO
    {
        public Guid Id { get; set; }
        public byte[] Picture { get; set; }
        public Guid DriverLicenseId { get; set; }

        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^= 31 + DriverLicenseId.ToString().ToInt();
        }
    }
}