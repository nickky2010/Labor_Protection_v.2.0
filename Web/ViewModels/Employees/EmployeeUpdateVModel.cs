using BLL;
using System;
using System.ComponentModel.DataAnnotations;
using Web.Interfaces;
using Web.ViewModels.DriverLicenses;
using Web.ViewModels.DriverMedicalCertificates;
using Web.ViewModels.Positions;

namespace Web.ViewModels.Employees
{
    public class EmployeeUpdateVModel : IUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }
    }
}
