using System;
using Web.Interfaces;

namespace Web.ViewModels.DriverCategories
{
    public class DriverCategoryUpdateVModel : IUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}