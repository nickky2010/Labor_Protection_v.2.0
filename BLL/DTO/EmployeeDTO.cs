using BLL.Interfaces;
using DAL.Extentions;
using System;

namespace BLL.DTO
{
    public class EmployeeDTO : IDataDTO
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public PositionDTO Position { get; set; }
        public DriverMedicalCertificateDTO DriverMedicalCertificate { get; set; }
        public DriverLicenseDTO DriverLicense { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EmployeeDTO)) return false;
            EmployeeDTO e = (EmployeeDTO)obj;
            return GetHashCode() == e.GetHashCode();
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            hash ^= 31 + Surname.ToInt();
            hash ^= 31 + FirstName.ToInt();
            return hash ^ 31 + Patronymic.ToInt();
        }
    }
}
