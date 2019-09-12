using System;
using System.Collections.Generic;
using Web.ViewModels.DriverCategories;
using Web.ViewModels.DriverMedicalCertificatesPhotos;

namespace Web.ViewModels.DriverMedicalCertificates
{
    public class DriverMedicalCertificateForEmployeeVModel
    {
        public Guid Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public IList<DriverCategoryAddVModel> DriverCategories { get; set; }
        public IList<DriverMedicalCertificatePhotoDMCAddVModel> Photos { get; set; }
    }
}