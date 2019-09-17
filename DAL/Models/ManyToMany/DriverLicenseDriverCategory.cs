using DAL.Extentions;
using System;

namespace DAL.Models.ManyToMany
{
    public class DriverLicenseDriverCategory 
    {
        public Guid DriverLicenseId { get; set; }
        public virtual DriverLicense DriverLicense { get; set; }
        public Guid DriverCategoryId { get; set; }
        public virtual DriverCategory DriverCategory { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DriverLicenseDriverCategory)) return false;
            DriverLicenseDriverCategory tDTO = (DriverLicenseDriverCategory)obj;
            return GetHashCode() == tDTO.GetHashCode();
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + DriverLicenseId.ToString().ToInt();
            return hash ^= 31 + DriverCategoryId.ToString().ToInt();
        }
    }
}
