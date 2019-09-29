using DAL.Extentions;
using DAL.Interfaces;
using System;

namespace DAL.Models
{
    public class DriverMedicalCertificatePhoto : AbstractData<DriverMedicalCertificatePhoto>, IPhotoData
    {
        public Guid DriverMedicalCertificateId { get; set; }
        public virtual DriverMedicalCertificate DriverMedicalCertificate { get; set; }
        public byte[] Photo { get; set; }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^= 31 + DriverMedicalCertificateId.ToString().ToInt();
        }
    }
}
