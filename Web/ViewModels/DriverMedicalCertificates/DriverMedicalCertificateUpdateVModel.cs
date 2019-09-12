using System;
using System.Collections.Generic;
using Web.Interfaces;
using Web.ViewModels.DriverMedicalCertificatesPhotos;

namespace Web.ViewModels.DriverMedicalCertificates
{
    public class DriverMedicalCertificateUpdateVModel : IUpdateViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public Guid EmployeeId { get; set; }
        public IList<Guid> DriverCategoriesId { get; set; }
        public IList<DriverMedicalCertificatePhotoUpdateVModel> Photos { get; set; }
    }
}