using System;
using System.Collections.Generic;
using Web.Interfaces;
using Web.ViewModels.DriverCategories;
using Web.ViewModels.DriverMedicalCertificatesPhotos;
using Web.ViewModels.Employees;

namespace Web.ViewModels.DriverMedicalCertificates
{
    public class DriverMedicalCertificateGetVModel : IGetViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public EmployeeForModelsVModel Employee { get; set; }
        public IList<DriverCategoryForModelsVModel> DriverCategories { get; set; }
        public IList<DriverMedicalCertificatePhotoGetVModel> Photos { get; set; }
    }
}