using DAL.Extentions;
using System;

namespace BLL.DTO
{
    public class DriverMedicalCertificatePhotoDTO
    {
        public Guid Id { get; set; }
        public DriverMedicalCertificateDTO DriverMedicalCertificate { get; set; }
        public byte[] Picture { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DriverMedicalCertificatePhotoDTO)) return false;
            DriverMedicalCertificatePhotoDTO d = (DriverMedicalCertificatePhotoDTO)obj;
            return GetHashCode() == d.GetHashCode();
        }
        public override int GetHashCode()
        {
            int hash = 17;
            return hash ^= 31 + Id.ToString().ToInt();
        }
    }
}