using BLL;
using DAL.Extentions;
using System.ComponentModel.DataAnnotations;
using Web.Interfaces;

namespace Web.ViewModels.Identity
{
    public class LoginViewModel : IViewModel
    {
        //[Display(Name = "Login", ResourceType = typeof(SharedResource))]
        //[RegularExpression(@"^[A-Z]{1}[a-z]{3,10}$", ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "ValidateLogin")]
        //[Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "EnterLogin")]
        public string Login { get; set; }

        //[Display(Name = "Password", ResourceType = typeof(SharedResource))]
        //[Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "EnterPassword")]
        //[StringLength(10, MinimumLength = 6, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "ValidateStringLengthPassword")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
