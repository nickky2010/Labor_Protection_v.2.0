using AutoMapper;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    internal abstract class AbstractUploadDataFromFileService<FileType, ReadModel> : AbstractBaseService,
        IUploadDataFromFileService<FileType, ReadModel>
        where FileType : class
        where ReadModel : IReadModel
    {
        public AbstractUploadDataFromFileService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper) { }

        protected IReader<FileType, ReadModel> Reader { get; set; }
        protected IValidatorOfUploadFile<FileType> Validator { get; set; }

        public abstract Task<IAppActionResult> SynchronizeData(IFormFile file);
    }
}
