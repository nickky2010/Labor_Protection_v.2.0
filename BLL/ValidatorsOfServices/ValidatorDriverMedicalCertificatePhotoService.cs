using BLL.DTO.DriverMedicalCertificatePhotos;
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
    internal class ValidatorDriverMedicalCertificatePhotoService : 
        AbstractValidatorOfCRUDDataBaseServices<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto>
    {
        protected override string EntityAlreadyExist { get => "DriverMedicalCertificatePhotoAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverMedicalCertificatePhotoNotFound"; }
        protected override string EntitiesNotFound { get => "DriverMedicalCertificatePhotosNotFound"; }
        IValidatorUploadDataFromFileForCRUDService<Image> ValidatorUploadDataFromFile { get; set; }

        public ValidatorDriverMedicalCertificatePhotoService(IUnitOfWork<LaborProtectionContext> unitOfWork)
            :base(unitOfWork)
        {
            ValidatorUploadDataFromFile = new ValidatorPhotoFile<Image>(unitOfWork);
        }

        public override async Task<IAppActionResult<DriverMedicalCertificatePhotoGetDTO>> ValidateAdd(DriverMedicalCertificatePhoto data, DriverMedicalCertificatePhotoAddDTO model,
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

        public override async Task<IAppActionResult<DriverMedicalCertificatePhotoGetDTO>> ValidateUpdate(DriverMedicalCertificatePhoto data, DriverMedicalCertificatePhotoUpdateDTO model,
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

        protected override async Task<IAppActionResult<DriverMedicalCertificatePhotoGetDTO>> ValidateConnectedAddEntities(DriverMedicalCertificatePhoto data,
            DriverMedicalCertificatePhotoAddDTO model, IStringLocalizer<SharedResource> localizer)
        {
            if (!await UnitOfWork.DriverMedicalCertificates.IsIdExistAsync(model.DriverMedicalCertificateId))
                GetResult.ErrorMessages.Add(localizer["DriverMedicalCertificateNotFound"]);
            return GetResult;
        }

        protected override async Task<IAppActionResult<DriverMedicalCertificatePhotoGetDTO>> ValidateConnectedUpdateEntities(DriverMedicalCertificatePhoto data, 
            DriverMedicalCertificatePhotoUpdateDTO model, IStringLocalizer<SharedResource> localizer)
        {
            if (!await UnitOfWork.DriverMedicalCertificates.IsIdExistAsync(model.DriverMedicalCertificateId))
                GetResult.ErrorMessages.Add(localizer["DriverMedicalCertificateNotFound"]);
            return GetResult;
        }
    }
}
