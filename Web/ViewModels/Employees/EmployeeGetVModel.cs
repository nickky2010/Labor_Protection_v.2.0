using System;
using Web.Interfaces;
using Web.ViewModels.DriverLicenses;
using Web.ViewModels.DriverMedicalCertificates;

namespace Web.ViewModels.Employees
{
    public class EmployeeGetVModel : IGetViewModel
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }
        public DriverMedicalCertificateForEmployeeVModel DriverMedicalCertificate { get; set; }
        public DriverLicenseForEmployeeVModel DriverLicense { get; set; }
    }
}
