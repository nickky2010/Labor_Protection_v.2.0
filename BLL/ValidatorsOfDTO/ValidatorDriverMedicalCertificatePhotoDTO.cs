﻿using BLL.DTO.DriverMedicalCertificatePhotos;
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
using Microsoft.AspNetCore.Http;
using BLL.Infrastructure.Extentions;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorDriverMedicalCertificatePhotoDTO : 
        AbstractValidatorDTO<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto>
    {
        protected override string EntityAlreadyExist { get => "DriverMedicalCertificatePhotoAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverMedicalCertificatePhotoNotFound"; }
        protected override string EntitiesNotFound { get => "DriverMedicalCertificatePhotosNotFound"; }

        public ValidatorDriverMedicalCertificatePhotoDTO(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }

        public override async Task<IAppActionResult> ValidateAdd(DriverMedicalCertificatePhotoAddDTO model)
        {
            var result = await base.ValidateAdd(model);
            ValidateConnected(result, model.DriverMedicalCertificateId, model.Picture);
            return result;
        }
        public override async Task<IAppActionResult<DriverMedicalCertificatePhoto>> ValidateUpdate(DriverMedicalCertificatePhotoUpdateDTO model)
        {
            var result = await base.ValidateUpdate(model);
            ValidateConnected(result, model.DriverMedicalCertificateId, model.Picture);
            return result;
        }
        protected override Task<DriverMedicalCertificatePhoto> FindDataAsync(Guid id) =>
            UnitOfWork.DriverMedicalCertificatePhotos.FindAsync(x => x.Id == id);

        protected override Task<List<DriverMedicalCertificatePhoto>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.DriverMedicalCertificatePhotos.GetPageAsync(startItem, countItem);

        protected override Task<DriverMedicalCertificatePhoto> FindDataAsync(DriverMedicalCertificatePhotoAddDTO modelDTO) =>
            UnitOfWork.DriverMedicalCertificatePhotos.FindAsync(x => x.DriverMedicalCertificateId == modelDTO.DriverMedicalCertificateId);
        protected override Task<int> GetCountElementAsync() => UnitOfWork.DriverMedicalCertificatePhotos.CountElementAsync();

        private async void ValidateConnected(IAppActionResult result, Guid id, IFormFile file)
        {
            if (!await UnitOfWork.DriverMedicalCertificates.IsIdExistAsync(id))
                result.ErrorMessages.Add(Localizer["DriverMedicalCertificateNotFound"]);
            IValidatorOfUploadFile<Image> validatorFile = new ValidatorPhotoFile(UnitOfWork, Localizer);
            result.AddErrors(validatorFile.ValidateFile(file));
            result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
        }
    }
}
