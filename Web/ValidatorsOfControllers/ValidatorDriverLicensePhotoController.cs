using BLL.DTO.DriverLicensePhotos;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Net;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverLicensePhotoController : 
        AbstractValidatorOfControllers<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO>
    {
        public ValidatorDriverLicensePhotoController(IStringLocalizer<SharedResource> localizer):base(localizer) { }

        public override IAppActionResult<DriverLicensePhotoGetDTO> ValidateAdd(DriverLicensePhotoAddDTO addDTO, ModelStateDictionary modelState)
        {
            if (addDTO == null)
                GetResult.ErrorMessages.Add(Localizer[NoData]);
            else if (addDTO.Picture.Length == 0)
                GetResult.ErrorMessages.Add(Localizer["NoPhoto"]);            
            if (!modelState.IsValid)
                GetResult.ErrorMessages.Add(Localizer[DataIsNotValid]);
            SetStatus(GetResult, HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return GetResult;
        }

        public override IAppActionResult<DriverLicensePhotoGetDTO> ValidateUpdate(DriverLicensePhotoUpdateDTO updateDTO, ModelStateDictionary modelState)
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
