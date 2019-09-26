using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUploadDataFromFileService<FileType, ReadModel> : IService
        where FileType : class
        where ReadModel : IReadModel
    {
        Task<IAppActionResult> SynchronizeData(IFormFile file);
    }
}
