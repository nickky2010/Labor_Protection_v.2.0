using System;
using System.Collections.Generic;
using Web.Interfaces;
using Web.ViewModels.DriverLicenses;
using Web.ViewModels.DriverMedicalCertificates;

namespace Web.ViewModels.DriverCategories
{
    public class DriverCategoryGetVModel : IGetViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<DriverLicenseForCategoryVModel> DriverLicenses { get; set; }
        public IList<DriverMedicalCertificateForCategoryVModel> DriverMedicalCertificates { get; set; }
    }
}