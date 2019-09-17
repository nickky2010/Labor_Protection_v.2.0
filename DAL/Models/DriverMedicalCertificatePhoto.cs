using DAL.Extentions;
using System;

namespace DAL.Models
{
    public class DriverMedicalCertificatePhoto : AbstractData<DriverMedicalCertificatePhoto>
    {
        public Guid DriverMedicalCertificateId { get; set; }
        public virtual DriverMedicalCertificate DriverMedicalCertificate { get; set; }
        public byte[] Picture { get; set; }
        public byte[] RowVersion { get; set; }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^= 31 + DriverMedicalCertificateId.ToString().ToInt();
        }
    }
}
