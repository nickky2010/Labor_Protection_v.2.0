using BLL.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Web.Interfaces
{
    public interface IValidatorFileController
    {
        IAppActionResult ValidateFile(IFormFile file);
    }
}
