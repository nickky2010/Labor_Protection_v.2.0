using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Drawing;

namespace BLL.ValidatorsOfServices.Abstract
{
    internal class ValidatorPhotoFile<FileType> : AbstractBaseValidator,
        IValidatorUploadDataFromFileForCRUDService<Image>
        where FileType : Image
    {
        public IAppActionResult<Image> ResultFileType { get; set; }

        public ValidatorPhotoFile(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer)
        {
            ResultFileType = new AppActionResult<Image>();
        }

        public string ErrorMessage => "FileNotPicture";

        public IAppActionResult<Image> ValidateFile(IFormFile file, IAppActionResult result)
        {
            try
            {
                using (ResultFileType.Data = Image.FromStream(file.OpenReadStream()))
                {
                }
            }
            catch
            {
                result.ErrorMessages.Add(Localizer[ErrorMessage]);
            }
            SetStatus(ResultFileType, System.Net.HttpStatusCode.BadRequest, System.Net.HttpStatusCode.OK);
            return ResultFileType;
        }
    }
}
