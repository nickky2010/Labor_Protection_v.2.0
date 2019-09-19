using AutoMapper;
using BLL.Interfaces;
using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Controllers.Abstract
{
    public abstract class AbstractFileController<FileType, ReadModel> : BaseController, IFileController<FileType, ReadModel>
        where FileType : class
        where ReadModel : IReadModel
    {
        public IUploadDataFromFileService<FileType, ReadModel> Service { get; set; }
        public IValidatorFileController Validator { get; set; }

        public AbstractFileController(IStringLocalizer<SharedResource> localizer, IMapper mapper,
            IUploadDataFromFileService<FileType, ReadModel> service): base(localizer, mapper) 
        {
            Service = service;
            Service.Localizer = localizer;
        }

        [HttpPut]
        public virtual async Task<IAppActionResult> SynchronizeData(IFormFile file)
        {
            var result = Validator.ValidateFile(file);
            if (!result.IsSuccess)
                return result;
            return SendResult(await Service.SynchronizeData(file));
        }
    }
}
