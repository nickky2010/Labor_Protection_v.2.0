using BLL.DTO.DriverLicensePhotos;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using BLL.ValidatorsOfServices.Abstract;
using System.Net;
using System.Drawing;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverLicensePhotoService: 
        AbstractValidatorOfCRUDDataBaseServices<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto>
    {
        protected override string EntityAlreadyExist { get => "DriverLicensePhotoAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverLicensePhotoNotFound"; }
        protected override string EntitiesNotFound { get => "DriverLicensePhotosNotFound"; }
        IValidatorUploadDataFromFileForCRUDService<Image> ValidatorUploadDataFromFile { get; set; }

        public ValidatorDriverLicensePhotoService(IUnitOfWork<LaborProtectionContext> unitOfWork)
            :base(unitOfWork)
        {
            ValidatorUploadDataFromFile = new ValidatorPhotoFile<Image>(unitOfWork);
        }

        public override async Task<IAppActionResult<DriverLicensePhotoGetDTO>> ValidateAdd(DriverLicensePhoto data, DriverLicensePhotoAddDTO model,
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer)
        {
            ValidatorUploadDataFromFile.ValidateFile(model.Picture, GetResult, localizer);
            if (data != null)
                GetResult.ErrorMessages.Add(localizer[EntityAlreadyExist]);
            else
                GetResult = await ValidateConnectedAddEntities(data, model, localizer);
            SetStatus(GetResult, statusCodeIsError, statusCodeIsSuccess);
            return GetResult;
        }

        public override async Task<IAppActionResult<DriverLicensePhotoGetDTO>> ValidateUpdate(DriverLicensePhoto data, DriverLicensePhotoUpdateDTO model,
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer)
        {
            ValidatorUploadDataFromFile.ValidateFile(model.Picture, GetResult, localizer);
            if (data == null)
                GetResult.ErrorMessages.Add(localizer[EntityNotFound]);
            else
                GetResult = await ValidateConnectedUpdateEntities(data, model, localizer);
            SetStatus(GetResult, statusCodeIsError, statusCodeIsSuccess);
            return GetResult;
        }

        protected override async Task<IAppActionResult<DriverLicensePhotoGetDTO>> ValidateConnectedAddEntities(DriverLicensePhoto data, 
            DriverLicensePhotoAddDTO model, IStringLocalizer<SharedResource> localizer)
        {
            if (!await UnitOfWork.DriverLicenses.IsIdExistAsync(model.DriverLicenseId))
                GetResult.ErrorMessages.Add(localizer["DriverLicenseNotFound"]);
            return GetResult;
        }

        protected override async Task<IAppActionResult<DriverLicensePhotoGetDTO>> ValidateConnectedUpdateEntities(DriverLicensePhoto data, 
            DriverLicensePhotoUpdateDTO model, IStringLocalizer<SharedResource> localizer)
        {
            if (!await UnitOfWork.DriverLicenses.IsIdExistAsync(model.DriverLicenseId))
                GetResult.ErrorMessages.Add(localizer["DriverLicenseNotFound"]);
            return GetResult;
        }
    }
}
