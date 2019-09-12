using System;
using System.Collections.Generic;
using Web.ViewModels.DriverCategories;
using Web.ViewModels.DriverLicensePhotos;

namespace Web.ViewModels.DriverLicenses
{
    public class DriverLicenseForEmployeeVModel
    {
        public Guid Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public IList<DriverCategoryAddVModel> DriverCategories { get; set; }
        public IList<DriverLicensePhotoForDLAddVModel> Photos { get; set; }
    }
}