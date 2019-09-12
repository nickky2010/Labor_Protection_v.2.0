using System;
using System.Collections.Generic;
using Web.Interfaces;
using Web.ViewModels.DriverCategories;
using Web.ViewModels.DriverLicensePhotos;
using Web.ViewModels.Employees;

namespace Web.ViewModels.DriverLicenses
{
    public class DriverLicenseGetVModel : IGetViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public EmployeeForModelsVModel Employee { get; set; }
        public IList<DriverCategoryForModelsVModel> DriverCategories { get; set; }
        public IList<DriverLicensePhotoGetVModel> Photos { get; set; }
    }
}