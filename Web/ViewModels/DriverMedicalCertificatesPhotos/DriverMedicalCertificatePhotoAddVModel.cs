using System;
using Web.Interfaces;

namespace Web.ViewModels.DriverMedicalCertificatesPhotos
{
    public class DriverMedicalCertificatePhotoAddVModel : IAddViewModel
    {
        public Guid DriverMedicalCertificateId { get; set; }
        public byte[] Picture { get; set; }
    }
}