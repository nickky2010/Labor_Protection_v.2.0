using DAL.Extentions;
using DAL.Models.ManyToMany;
using System.Collections.Generic;

namespace DAL.Models
{
    public class DriverCategory: AbstractData<DriverCategory>
    {
        public string Name { get; set; }
        public virtual IList<DriverLicenseDriverCategory> DriverLicenseDriverCategories { get; set; }
        public virtual IList<DriverMedicalCertificateDriverCategory> DriverMedicalCertificateDriverCategories { get; set; }

        public byte[] RowVersion { get; set; }
        public DriverCategory()
        {
            DriverLicenseDriverCategories = new List<DriverLicenseDriverCategory>();
            DriverMedicalCertificateDriverCategories = new List<DriverMedicalCertificateDriverCategory>();
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^= 31 + Name.ToInt();
        }
    }
}
