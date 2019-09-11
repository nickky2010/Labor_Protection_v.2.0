using BLL;
using System.ComponentModel.DataAnnotations;
using Web.Interfaces;

namespace Web.ViewModels.Positions
{
    public class PositionForEmployeeViewModel : IViewModel
    {
        //[Display(Name = "Position", ResourceType = typeof(SharedResource))]
        //[RegularExpression(@"^[A-ZА-ЯЁ]{1}[a-zа-яё]{1,30}$", ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "ValidatePosition")]
        //[Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "EnterPosition")]
        public string Name { get; set; }
    }
}