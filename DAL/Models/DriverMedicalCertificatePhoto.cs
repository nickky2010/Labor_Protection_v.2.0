using DAL.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class DriverMedicalCertificatePhoto
    {
        public Guid Id { get; set; }
        public Guid DriverMedicalCertificateId { get; set; }
        public virtual DriverMedicalCertificate DriverMedicalCertificate { get; set; }
        public byte[] Picture { get; set; }
        public byte[] RowVersion { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DriverMedicalCertificatePhoto)) return false;
            DriverMedicalCertificatePhoto d = (DriverMedicalCertificatePhoto)obj;
            return Id.Equals(d.Id);
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^= 31 + DriverMedicalCertificateId.ToString().ToInt();
        }
    }
}
