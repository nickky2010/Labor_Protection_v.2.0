using BLL;
using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.Infrastructure.Extentions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Net;
using Web.ValidatorsOfControllers.Abstract;
using Microsoft.AspNetCore.Http;
using BLL.Infrastructure.Extentions;

namespace Web.ValidatorsOfControllers
{
    internal class ValidatorDriverMedicalCertificatePhotoController :
        AbstractValidatorOfControllers<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO>
    {
        public ValidatorDriverMedicalCertificatePhotoController(IStringLocalizer<SharedResource> localizer)
            :base(localizer) { }

        public override IAppActionResult<DriverMedicalCertificatePhotoGetDTO> ValidateAdd(DriverMedicalCertificatePhotoAddDTO addDTO, ModelStateDictionary modelState)
        {
            var result = base.ValidateAdd(addDTO, modelState);
            if (addDTO != null)
                ValidateConnected(result, addDTO.Picture);
            return result;
        }

        public override IAppActionResult<DriverMedicalCertificatePhotoGetDTO> ValidateUpdate(DriverMedicalCertificatePhotoUpdateDTO updateDTO, ModelStateDictionary modelState)
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
