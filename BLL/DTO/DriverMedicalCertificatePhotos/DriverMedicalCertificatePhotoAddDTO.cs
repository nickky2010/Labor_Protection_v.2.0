﻿using BLL.Interfaces;
using DAL.Extentions;
using Microsoft.AspNetCore.Http;
using System;

namespace BLL.DTO.DriverMedicalCertificatePhotos
{
    public class DriverMedicalCertificatePhotoAddDTO : AbstractDataDTO<DriverMedicalCertificatePhotoAddDTO>, IAddDTO, IAddUpdatePhotoDTO
    {
        public IFormFile Picture { get; set; }
        public Guid DriverMedicalCertificateId { get; set; }

        public override int GetHashCode()
        {
            int hash = 17;
            return hash ^= 31 + DriverMedicalCertificateId.ToString().ToInt();
        }
    }
}