using System;
using Web.Interfaces;

namespace Web.ViewModels.DriverLicensePhotos
{
    public class DriverLicensePhotoGetVModel : IGetViewModel
    {
        public Guid Id { get; set; }
        public byte[] Picture { get; set; }
    }
}