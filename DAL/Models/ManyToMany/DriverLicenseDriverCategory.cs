using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.ManyToMany
{
    public class DriverLicenseDriverCategory
    {
        public Guid DriverLicenseId { get; set; }
        public virtual DriverLicense DriverLicense { get; set; }
        public Guid DriverCategoryId { get; set; }
        public virtual DriverCategory DriverCategory { get; set; }
    }
}
