using System;
using System.Collections.Generic;
using Web.Interfaces;
using Web.ViewModels.DriverLicensePhotos;

namespace Web.ViewModels.DriverLicenses
{
    public class DriverLicenseUpdateVModel : IUpdateViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public Guid EmployeeId { get; set; }
        public IList<Guid> DriverCategoriesId { get; set; }
        public IList<DriverLicensePhotoUpdateVModel> Photos { get; set; }
    }
}