using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Web.Interfaces
{
    public interface IFileController<FileType, ReadModel>
        where FileType : class
        where ReadModel : IReadModel
    {
        IUploadDataFromFileService<FileType, ReadModel> Service { get; set; }
        IValidatorFileController Validator { get; set; }

        Task<IAppActionResult> SynchronizeData(IFormFile file);
    }
}
