using BLL;
using System;
using System.ComponentModel.DataAnnotations;
using Web.Interfaces;
using Web.ViewModels.DriverLicenses;
using Web.ViewModels.DriverMedicalCertificates;
using Web.ViewModels.Positions;

namespace Web.ViewModels.Employees
{
    public class EmployeeViewModel : IViewModel
    {
        public Guid Id { get; set; }

        //[Display(ResourceType = typeof(SharedResource), Name = "Surname")]
        //[RegularExpression(@"^[A-ZА-ЯЁ]{1}[a-zа-яё]{1,30}$", ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "ValidateSurname")]
        //[Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "EnterSurname")]
        public string Surname { get; set; }

        //[Display(Name = "FirstName", ResourceType = typeof(SharedResource))]
        //[RegularExpression(@"^[A-ZА-ЯЁ]{1}[a-zа-яё]{1,30}$", ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "ValidateFirstName")]
        //[Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "EnterFirstName")]
        public string FirstName { get; set; }

        //[Display(Name = "Patronymic", ResourceType = typeof(SharedResource))]
        //[RegularExpression(@"^[A-ZА-ЯЁ]{1}[a-zа-яё]{1,30}$", ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "ValidatePatronymic")]
        //[Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "EnterPatronymic")]
        public string Patronymic { get; set; }

        //[Display(Name = "Position", ResourceType = typeof(SharedResource))]
        //[Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "EnterPosition")]
        public PositionForEmployeeViewModel Position { get; set; }

        //[Display(Name = "DriverMedicalCertificate", ResourceType = typeof(SharedResource))]
        public DriverMedicalCertificateForEmployeeViewModel DriverMedicalCertificate { get; set; }

        //[Display(Name = "DriverLicense", ResourceType = typeof(SharedResource))]
        public DriverLicenseForEmployeeViewModel DriverLicense { get; set; }
    }
}
