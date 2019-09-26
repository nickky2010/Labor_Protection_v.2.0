using AutoMapper;
using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Controllers.Abstract
{
    public abstract class AbstractFileController<FileType, ReadModel> : BaseController, IFileController
        where FileType : class
        where ReadModel : IReadModel
    {
        protected IUploadDataFromFileService<FileType, ReadModel> Service { get; private set; }
        protected IValidatorFileController Validator { get; set; }

        public AbstractFileController(IStringLocalizer<SharedResource> localizer, IMapper mapper,
            IUploadDataFromFileService<FileType, ReadModel> service) : base(localizer, mapper)
        {
            Service = service;
            Service.Localizer = localizer;
        }

        [HttpPut]
        public virtual async Task<IAppActionResult> SynchronizeData(IFormFile file)
        {
            var result = Validator.ValidateFile(file);
            SetResponseStatusCode(result);
            if (!result.IsSuccess)
                return result;
            result = await Service.SynchronizeData(file);
            SetResponseStatusCode(result);
            return result;
        }
    }
}
