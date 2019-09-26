using DAL.Extentions;
using System;

namespace DAL.Models
{
    public class DriverLicensePhoto : AbstractData<DriverLicensePhoto>
    {
        public Guid DriverLicenseId { get; set; }
        public virtual DriverLicense DriverLicense { get; set; }
        public byte[] Photo { get; set; }
        public byte[] RowVersion { get; set; }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^= 31 + DriverLicenseId.ToString().ToInt();
        }
    }
}
