using DAL.Interfaces;
using System;

namespace DAL.Models.ManyToMany
{
    public class DriverLicenseDriverCategory: IDriverCategory
    {
        public Guid DriverLicenseId { get; set; }
        public virtual DriverLicense DriverLicense { get; set; }
        public Guid DriverCategoryId { get; set; }
        public virtual DriverCategory DriverCategory { get; set; }
    }
}
