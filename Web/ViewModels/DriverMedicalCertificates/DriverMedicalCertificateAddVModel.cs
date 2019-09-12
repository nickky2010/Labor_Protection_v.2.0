using System;
using System.Collections.Generic;
using Web.Interfaces;
using Web.ViewModels.DriverMedicalCertificatesPhotos;

namespace Web.ViewModels.DriverMedicalCertificates
{
    public class DriverMedicalCertificateAddVModel : IAddViewModel
    {
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public IList<Guid> DriverCategoriesId { get; set; }
        public IList<DriverMedicalCertificatePhotoDMCAddVModel> Photos { get; set; }
    }
}