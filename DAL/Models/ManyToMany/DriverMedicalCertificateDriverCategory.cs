using DAL.Interfaces;
using System;

namespace DAL.Models.ManyToMany
{
    public class DriverMedicalCertificateDriverCategory : IDriverCategory
    {
        public Guid DriverMedicalCertificateId { get; set; }
        public virtual DriverMedicalCertificate DriverMedicalCertificate { get; set; }
        public Guid DriverCategoryId { get; set; }
        public virtual DriverCategory DriverCategory { get; set; }
    }
}
