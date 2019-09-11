using BLL.Interfaces;
using DAL.Extentions;
using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class DriverMedicalCertificateDTO : IDataDTO
    {
        public Guid Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public ICollection<string> OpenCategories { get; set; }
        public ICollection<byte[]> CertificatePhotos { get; set; }
        public EmployeeDTO Employee { get; set; }
        public DriverMedicalCertificateDTO()
        {
            CertificatePhotos = new List<byte[]>();
            OpenCategories = new List<string>();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DriverMedicalCertificateDTO)) return false;
            DriverMedicalCertificateDTO d = (DriverMedicalCertificateDTO)obj;
            return GetHashCode() == d.GetHashCode();
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            hash ^= 31 + SerialNumber.ToInt();
            hash ^= 31 + DateOfIssue.ToShortDateString().ToInt();
            return hash ^ 31 + ExpiryDate.ToShortDateString().ToInt();
        }
    }
}
