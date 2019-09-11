﻿using DAL.Extentions;
using System;

namespace DAL.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public Guid PositionId { get; set; }
        public Guid? DriverMedicalCertificateId { get; set; }
        public Guid? DriverLicenseId { get; set; }

        public virtual Position Position { get; set; }
        public virtual DriverMedicalCertificate DriverMedicalCertificate { get; set; }
        public virtual DriverLicense DriverLicense { get; set; }

        public byte[] RowVersion { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Employee)) return false;
            Employee e = (Employee)obj;
            return Id.Equals(e.Id);
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