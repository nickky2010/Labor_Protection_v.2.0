using System;

namespace Web.ViewModels.DriverMedicalCertificates
{
    public class DriverMedicalCertificateForCategoryVModel
    {
        public Guid Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
    }
}
