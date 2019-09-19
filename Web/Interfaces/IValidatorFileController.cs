using BLL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Web.Interfaces
{
    public interface IValidatorFileController
    {
        IAppActionResult Result { get; set; }
        string NoData { get; }

        IAppActionResult ValidateFile(IFormFile file);
    }
}
