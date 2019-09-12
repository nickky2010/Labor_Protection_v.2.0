using Web.Interfaces;

namespace Web.ViewModels.Employees
{
    public class EmployeeAddVModel : IAddViewModel
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }
    }
}
