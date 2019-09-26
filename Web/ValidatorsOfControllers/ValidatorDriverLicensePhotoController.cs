using BLL;
using BLL.DTO.DriverLicensePhotos;
using BLL.Infrastructure.Extentions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Net;
using Web.ValidatorsOfControllers.Abstract;

namespace Web.ValidatorsOfControllers
{
    internal class ValidatorDriverLicensePhotoController :
        AbstractValidatorOfControllers<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO>
    {
        public ValidatorDriverLicensePhotoController(IStringLocalizer<SharedResource> localizer)
            : base(localizer) { }

        public override IAppActionResult<DriverLicensePhotoGetDTO> ValidateAdd(DriverLicensePhotoAddDTO addDTO, ModelStateDictionary modelState)
        {
            var result = base.ValidateAdd(addDTO, modelState);
            if (addDTO != null)
                ValidateConnected(result, addDTO.Picture);
            return result;
        }

        public override IAppActionResult<DriverLicensePhotoGetDTO> ValidateUpdate(DriverLicensePhotoUpdateDTO updateDTO, ModelStateDictionary modelState)
        {
            var result = base.ValidateUpdate(updateDTO, modelState);
            if (updateDTO != null)
                ValidateConnected(result, updateDTO.Picture);
            return result;
        }

        private void ValidateConnected(IAppActionResult result, IFormFile file)
        {
            if (file == null || file.Length == 0)
                result.ErrorMessages.Add(Localizer["NoPhoto"]);
            result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
        }
    }
}
