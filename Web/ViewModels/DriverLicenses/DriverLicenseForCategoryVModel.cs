using System;

namespace Web.ViewModels.DriverLicenses
{
    public class DriverLicenseForCategoryVModel 
    {
        public Guid Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
    }
}