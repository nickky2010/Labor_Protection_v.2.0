using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models.ManyToMany
{
    public class DriverMedicalCertificateDriverCategory
    {
        public Guid DriverMedicalCertificateId { get; set; }
        public virtual DriverMedicalCertificate DriverMedicalCertificate { get; set; }
        public Guid DriverCategoryId { get; set; }
        public virtual DriverCategory DriverCategory { get; set; }
    }
}
