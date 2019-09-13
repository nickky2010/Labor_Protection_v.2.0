using BLL.Interfaces;
using DAL.Extentions;

namespace BLL.DTO.DriverMedicalCertificatePhotos
{
    public class DriverMedicalCertificatePhotoAddDTO : AbstractDataDTO<DriverMedicalCertificatePhotoAddDTO>, IAddDTO
    {
        public byte[] Picture { get; set; }
        public override int GetHashCode()
        {
            int hash = 17;
            return hash ^= 31 + Picture.ToString().ToInt();
        }
    }
}