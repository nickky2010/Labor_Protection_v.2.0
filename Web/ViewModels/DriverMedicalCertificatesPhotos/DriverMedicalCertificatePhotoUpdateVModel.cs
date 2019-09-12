using System;
using Web.Interfaces;

namespace Web.ViewModels.DriverMedicalCertificatesPhotos
{
    public class DriverMedicalCertificatePhotoUpdateVModel : IUpdateViewModel
    {
        public Guid Id { get; set; }
        public byte[] Picture { get; set; }
    }
}