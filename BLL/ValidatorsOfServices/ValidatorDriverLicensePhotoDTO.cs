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
using System;
using System.Collections.Generic;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverLicensePhotoDTO: 
        AbstractValidatorDTO<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto>
    {
        protected override string EntityAlreadyExist { get => "DriverLicensePhotoAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverLicensePhotoNotFound"; }
        protected override string EntitiesNotFound { get => "DriverLicensePhotosNotFound"; }
        IValidatorUploadDataFromFileForCRUDService<Image> ValidatorUploadDataFromFile { get; set; }

        public ValidatorDriverLicensePhotoDTO(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) 
        {
            ValidatorUploadDataFromFile = new ValidatorPhotoFile<Image>(unitOfWork, localizer);
        }
        public override async Task<IAppActionResult> ValidateAdd(DriverLicensePhotoAddDTO model)
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
        public override async Task<IAppActionResult<DriverLicensePhoto>> ValidateUpdate(DriverLicensePhotoUpdateDTO model)
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

        protected override async Task<IAppActionResult<DriverLicensePhoto>> ValidateConnectedEntities(DriverLicensePhoto data, DriverLicensePhotoAddDTO model)
        {
            if (!await UnitOfWork.DriverLicenses.IsIdExistAsync(model.DriverLicenseId))
                DataResult.ErrorMessages.Add(Localizer["DriverLicenseNotFound"]);
            return DataResult;
        }
        protected override async Task<IAppActionResult<DriverLicensePhoto>> ValidateConnectedEntities(DriverLicensePhoto data, DriverLicensePhotoUpdateDTO model)
        {
            if (!await UnitOfWork.DriverLicenses.IsIdExistAsync(model.DriverLicenseId))
                DataResult.ErrorMessages.Add(Localizer["DriverLicenseNotFound"]);
            return DataResult;
        }

        protected override Task<DriverLicensePhoto> FindDataAsync(Guid id) =>
            UnitOfWork.DriverLicensePhotos.FindAsync(x => x.Id == id);

        protected override Task<List<DriverLicensePhoto>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.DriverLicensePhotos.GetPageAsync(startItem, countItem);

        protected override Task<DriverLicensePhoto> FindDataIfAddAsync(DriverLicensePhotoAddDTO modelDTO) =>
            UnitOfWork.DriverLicensePhotos.FindAsync(x => x.DriverLicenseId == modelDTO.DriverLicenseId);
    }
}
