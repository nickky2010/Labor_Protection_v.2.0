﻿using DAL.Extentions;
using DAL.Models.ManyToMany;
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class DriverMedicalCertificate : AbstractData<DriverMedicalCertificate>
    {
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public virtual IList<DriverMedicalCertificateDriverCategory> DriverMedicalCertificateDriverCategories { get; set; }
        public virtual IList<DriverMedicalCertificatePhoto> Photos { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public DriverMedicalCertificate()
        {
            Photos = new List<DriverMedicalCertificatePhoto>();
            DriverMedicalCertificateDriverCategories = new List<DriverMedicalCertificateDriverCategory>();
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
