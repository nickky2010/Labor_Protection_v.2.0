using DAL.Extentions;
using DAL.Models.ManyToMany;
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class DriverLicense : AbstractData<DriverLicense>
    {
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public virtual IList<DriverLicenseDriverCategory> DriverLicenseDriverCategories { get; set; }
        //public virtual IList<DriverLicensePhoto> Photos { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public byte[] RowVersion { get; set; }
        public DriverLicense()
        {
            //Photos = new List<DriverLicensePhoto>();
            DriverLicenseDriverCategories = new List<DriverLicenseDriverCategory>();
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            hash ^= 31 + SerialNumber.ToInt();
            hash ^= 31 + DateOfIssue.ToShortDateString().ToInt();
            return hash ^ 31 + ExpiryDate.ToShortDateString().ToInt();
        }
    }
}
