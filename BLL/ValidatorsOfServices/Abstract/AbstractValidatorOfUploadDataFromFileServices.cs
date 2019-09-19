using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfServices.Abstract
{
    internal abstract class AbstractValidatorOfUploadDataFromFileServices<FileType> : 
        AbstractBaseValidatorOfServices, IValidatorUploadDataFromFileService<FileType>
        where FileType : class
    {
        public IAppActionResult<FileType> ResultFileType { get; set; }

        public AbstractValidatorOfUploadDataFromFileServices(IUnitOfWork<LaborProtectionContext> unitOfWork)
            : base(unitOfWork)
        {
            ResultFileType = new AppActionResult<FileType>();
        }

        public abstract string ErrorMessage { get; }

        public abstract IAppActionResult<FileType> ValidateFile(IFormFile file, IStringLocalizer<SharedResource> localizer);
    }
}
