using BLL.Interfaces;
using DAL.Extentions;
using System;

namespace BLL.DTO.DriverMedicalCertificatePhotos
{
    public class DriverMedicalCertificatePhotoGetUpdateDTO : AbstractDataDTO<DriverMedicalCertificatePhotoGetUpdateDTO>, IUpdateDTO, IGetDTO
    {
        public Guid Id { get; set; }
        public byte[] Picture { get; set; }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^= 31 + Picture.ToString().ToInt();
        }
    }
}