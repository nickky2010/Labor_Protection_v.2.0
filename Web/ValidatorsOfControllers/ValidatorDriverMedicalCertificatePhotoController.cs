using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.Interfaces;
using BLL;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Net;
using Web.ValidatorsOfControllers.Abstract;

namespace Web.ValidatorsOfControllers
{
    internal class ValidatorDriverMedicalCertificatePhotoController : 
        AbstractValidatorOfControllers<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO>
    {
        public ValidatorDriverMedicalCertificatePhotoController(IStringLocalizer<SharedResource> localizer):base(localizer) { }

        public override IAppActionResult<DriverMedicalCertificatePhotoGetDTO> ValidateAdd(DriverMedicalCertificatePhotoAddDTO addDTO, ModelStateDictionary modelState)
        {
            if (addDTO == null)
                GetResult.ErrorMessages.Add(Localizer[NoData]);
            else if (addDTO.Picture == null || addDTO.Picture.Length == 0)
                GetResult.ErrorMessages.Add(Localizer["NoPhoto"]);            
            if (!modelState.IsValid)
                GetResult.ErrorMessages.Add(Localizer[DataIsNotValid]);
            SetStatus(GetResult, HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return GetResult;
        }

        public override IAppActionResult<DriverMedicalCertificatePhotoGetDTO> ValidateUpdate(DriverMedicalCertificatePhotoUpdateDTO updateDTO, ModelStateDictionary modelState)
        {
            if (updateDTO == null)
                GetResult.ErrorMessages.Add(Localizer[NoData]);
            else if (updateDTO.Picture.Length == 0)
                GetResult.ErrorMessages.Add(Localizer["NoPhoto"]);
            if (!modelState.IsValid)
                GetResult.ErrorMessages.Add(Localizer[DataIsNotValid]);
            SetStatus(GetResult, HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return GetResult;
        }
    }
}
