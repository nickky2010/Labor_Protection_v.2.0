using DAL.Extentions;
using System;

namespace BLL.DTO
{
    public class DriverLicensePhotoDTO
    {
        public Guid Id { get; set; }
        public DriverLicenseDTO DriverLicense { get; set; }
        public byte[] Picture { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DriverLicensePhotoDTO)) return false;
            DriverLicensePhotoDTO d = (DriverLicensePhotoDTO)obj;
            return GetHashCode() == d.GetHashCode();
        }
        public override int GetHashCode()
        {
            int hash = 17;
            return hash ^= 31 + Id.ToString().ToInt();
        }
    }
}