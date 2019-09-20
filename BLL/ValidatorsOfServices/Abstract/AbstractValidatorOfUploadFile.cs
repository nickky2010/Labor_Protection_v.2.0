using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfServices.Abstract
{
    internal abstract class AbstractValidatorOfUploadFile<FileType> : 
        AbstractBaseValidator, IValidatorOfUploadFile<FileType>
        where FileType : class
    {
        public IAppActionResult<FileType> ResultFileType { get; set; }

        public AbstractValidatorOfUploadFile(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer)
        {
            ResultFileType = new AppActionResult<FileType>();
        }

        public abstract string ErrorMessage { get; }

        public abstract IAppActionResult<FileType> ValidateFile(IFormFile file);
    }
}
