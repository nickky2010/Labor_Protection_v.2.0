using DAL.Extentions;
using System;

namespace DAL.Models
{
    public class DriverLicensePhoto
    {
        public Guid Id { get; set; }
        public Guid DriverLicenseId { get; set; }
        public virtual DriverLicense DriverLicense { get; set; }
        public byte[] Picture { get; set; }
        public byte[] RowVersion { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DriverLicensePhoto)) return false;
            DriverLicensePhoto d = (DriverLicensePhoto)obj;
            return Id.Equals(d.Id);
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^= 31 + DriverLicenseId.ToString().ToInt();
        }
    }
}
