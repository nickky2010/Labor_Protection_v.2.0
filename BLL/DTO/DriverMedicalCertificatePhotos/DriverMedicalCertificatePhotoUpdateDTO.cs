using BLL.Interfaces;
using DAL.Extentions;
using Microsoft.AspNetCore.Http;
using System;

namespace BLL.DTO.DriverMedicalCertificatePhotos
{
    public class DriverMedicalCertificatePhotoUpdateDTO : 
        AbstractDataDTO<DriverMedicalCertificatePhotoUpdateDTO>, IUpdateDTO, IGetDTO, IAddUpdatePhotoDTO
    {
        public Guid Id { get; set; }
        public IFormFile Picture { get; set; }
        public Guid DriverMedicalCertificateId { get; set; }

        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^= 31 + DriverMedicalCertificateId.ToString().ToInt();
        }
    }
}