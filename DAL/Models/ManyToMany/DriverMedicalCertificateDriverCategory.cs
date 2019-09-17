using DAL.Extentions;
using System;

namespace DAL.Models.ManyToMany
{
    public class DriverMedicalCertificateDriverCategory 
    {
        public Guid DriverMedicalCertificateId { get; set; }
        public virtual DriverMedicalCertificate DriverMedicalCertificate { get; set; }
        public Guid DriverCategoryId { get; set; }
        public virtual DriverCategory DriverCategory { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DriverMedicalCertificateDriverCategory)) return false;
            DriverMedicalCertificateDriverCategory tDTO = (DriverMedicalCertificateDriverCategory)obj;
            return GetHashCode() == tDTO.GetHashCode();
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + DriverMedicalCertificateId.ToString().ToInt();
            return hash ^= 31 + DriverCategoryId.ToString().ToInt();
        }
    }
}
