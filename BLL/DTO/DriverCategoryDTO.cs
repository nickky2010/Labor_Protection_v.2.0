using DAL.Extentions;
using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class DriverCategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<DriverLicenseDTO> DriverLicenses { get; set; }
        public IList<DriverMedicalCertificateDTO> DriverMedicalCertificates { get; set; }

        public DriverCategoryDTO()
        {
            DriverLicenses = new List<DriverLicenseDTO>();
            DriverMedicalCertificates = new List<DriverMedicalCertificateDTO>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DriverCategoryDTO)) return false;
            DriverCategoryDTO d = (DriverCategoryDTO)obj;
            return GetHashCode() == d.GetHashCode();
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^= 31 + Name.ToInt();
        }
    }
}