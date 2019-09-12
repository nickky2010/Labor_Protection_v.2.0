using System;
using System.Collections.Generic;
using Web.Interfaces;
using Web.ViewModels.DriverLicensePhotos;

namespace Web.ViewModels.DriverLicenses
{
    public class DriverLicenseAddVModel : IAddViewModel
    {
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public Guid EmployeeId { get; set; }
        public IList<Guid> DriverCategoriesId { get; set; }
        public IList<DriverLicensePhotoForDLAddVModel> Photos { get; set; }
    }
}