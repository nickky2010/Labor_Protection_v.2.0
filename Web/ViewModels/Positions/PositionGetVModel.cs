using System;
using System.Collections.Generic;
using Web.Interfaces;
using Web.ViewModels.Employees;

namespace Web.ViewModels.Positions
{
    public class PositionGetVModel : IGetViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<EmployeeForModelsVModel> Employees { get; set; }
    }
}