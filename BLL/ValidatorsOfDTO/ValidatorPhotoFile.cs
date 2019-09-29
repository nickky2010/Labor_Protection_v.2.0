using BLL.Infrastructure;
using BLL.Infrastructure.Extentions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Drawing;
using System.Net;

namespace BLL.ValidatorsOfDTO.Abstract
{
    internal class ValidatorPhotoFile : AbstractFileValidator,
        IValidatorOfUploadFile<Image>
    {
        public ValidatorPhotoFile(IStringLocalizer<SharedResource> localizer)
            : base(localizer) { }

        public IAppActionResult<Image> ValidateFile(IFormFile file)
        {
            IAppActionResult<Image> result = new AppActionResult<Image>();
            try
            {
                using (result.Data = Image.FromStream(file.OpenReadStream()))
                {
                }
            }
            catch
            {
                result.ErrorMessages.Add(Localizer["FileNotPicture"]);
            }
            result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return result;
        }
    }
}
