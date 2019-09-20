using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using BLL.ValidatorsOfDTO.Abstract;
using System.Net;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorDriverMedicalCertificatePhotoDTO : 
        AbstractValidatorDTO<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto>
    {
        protected override string EntityAlreadyExist { get => "DriverMedicalCertificatePhotoAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverMedicalCertificatePhotoNotFound"; }
        protected override string EntitiesNotFound { get => "DriverMedicalCertificatePhotosNotFound"; }
        IValidatorUploadDataFromFileForCRUDService<Image> ValidatorUploadDataFromFile { get; set; }

        public ValidatorDriverMedicalCertificatePhotoDTO(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer)
        {
            ValidatorUploadDataFromFile = new ValidatorPhotoFile<Image>(unitOfWork, localizer);
        }

        public override async Task<IAppActionResult> ValidateAdd(DriverMedicalCertificatePhotoAddDTO model)
        {
            ValidatorUploadDataFromFile.ValidateFile(model.Picture, DataResult);
            DataResult.Data = await FindDataIfAddAsync(model);
            if (DataResult.Data != null)
                DataResult.ErrorMessages.Add(Localizer[EntityAlreadyExist]);
            else
                DataResult = await ValidateConnectedEntities(DataResult.Data, model);
            SetStatus(DataResult, HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return DataResult;
        }
        public override async Task<IAppActionResult<DriverMedicalCertificatePhoto>> ValidateUpdate(DriverMedicalCertificatePhotoUpdateDTO model)
        {
            ValidatorUploadDataFromFile.ValidateFile(model.Picture, DataResult);
            DataResult.Data = await FindDataAsync(model.Id);
            if (DataResult.Data == null)
                DataResult.ErrorMessages.Add(Localizer[EntityNotFound]);
            else
                DataResult = await ValidateConnectedEntities(DataResult.Data, model);
            SetStatus(DataResult, HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return DataResult;
        }

        protected override async Task<IAppActionResult<DriverMedicalCertificatePhoto>> ValidateConnectedEntities(DriverMedicalCertificatePhoto data, DriverMedicalCertificatePhotoAddDTO model)
        {
            if (!await UnitOfWork.DriverLicenses.IsIdExistAsync(model.DriverMedicalCertificateId))
                DataResult.ErrorMessages.Add(Localizer["DriverMedicalCertificateNotFound"]);
            return DataResult;
        }
        protected override async Task<IAppActionResult<DriverMedicalCertificatePhoto>> ValidateConnectedEntities(DriverMedicalCertificatePhoto data, DriverMedicalCertificatePhotoUpdateDTO model)
        {
            if (!await UnitOfWork.DriverLicenses.IsIdExistAsync(model.DriverMedicalCertificateId))
                DataResult.ErrorMessages.Add(Localizer["DriverMedicalCertificateNotFound"]);
            return DataResult;
        }
        protected override Task<DriverMedicalCertificatePhoto> FindDataAsync(Guid id) =>
            UnitOfWork.DriverMedicalCertificatePhotos.FindAsync(x => x.Id == id);

        protected override Task<List<DriverMedicalCertificatePhoto>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.DriverMedicalCertificatePhotos.GetPageAsync(startItem, countItem);

        protected override Task<DriverMedicalCertificatePhoto> FindDataIfAddAsync(DriverMedicalCertificatePhotoAddDTO modelDTO) =>
            UnitOfWork.DriverMedicalCertificatePhotos.FindAsync(x => x.DriverMedicalCertificateId == modelDTO.DriverMedicalCertificateId);
    }
}
