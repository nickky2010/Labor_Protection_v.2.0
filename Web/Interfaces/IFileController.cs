using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Web.Interfaces
{
    public interface IFileController
    {
        Task<IAppActionResult> SynchronizeData(IFormFile file);
    }
}
