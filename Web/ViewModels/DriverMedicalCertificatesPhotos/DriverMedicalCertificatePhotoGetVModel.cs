using System;
using Web.Interfaces;

namespace Web.ViewModels.DriverMedicalCertificatesPhotos
{
    public class DriverMedicalCertificatePhotoGetVModel : IGetViewModel
    {
        public Guid Id { get; set; }
        public byte[] Picture { get; set; }
    }
}