using System;
using Web.Interfaces;

namespace Web.ViewModels.DriverLicensePhotos
{
    public class DriverLicensePhotoAddVModel : IAddViewModel
    {
        public Guid DriverLicenseId { get; set; }
        public byte[] Picture { get; set; }
    }
}