using BLL.DTO.DriverLicensePhotos;
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
using BLL.Infrastructure.Extentions;
using Microsoft.AspNetCore.Http;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorDriverLicensePhotoDTO: 
        AbstractValidatorDTO<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto>
    {
        protected override string EntityAlreadyExist { get => "DriverLicensePhotoAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverLicensePhotoNotFound"; }
        protected override string EntitiesNotFound { get => "DriverLicensePhotosNotFound"; }

        public ValidatorDriverLicensePhotoDTO(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }

        public override async Task<IAppActionResult> ValidateAdd(DriverLicensePhotoAddDTO model)
        {
            var result = await base.ValidateAdd(model);
            ValidateConnected(result, model.DriverLicenseId, model.Picture);
            return result;            
        }

        public override async Task<IAppActionResult<DriverLicensePhoto>> ValidateUpdate(DriverLicensePhotoUpdateDTO model)
        {
            var result = await base.ValidateUpdate(model);
            ValidateConnected(result, model.DriverLicenseId, model.Picture);
            return result;
        }

        protected override Task<DriverLicensePhoto> FindDataAsync(Guid id) =>
            UnitOfWork.DriverLicensePhotos.FindAsync(x => x.Id == id);

        protected override Task<List<DriverLicensePhoto>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.DriverLicensePhotos.GetPageAsync(startItem, countItem);

        protected override Task<DriverLicensePhoto> FindDataAsync(DriverLicensePhotoAddDTO modelDTO) =>
            UnitOfWork.DriverLicensePhotos.FindAsync(x => x.DriverLicenseId == modelDTO.DriverLicenseId);
        protected override Task<int> GetCountElementAsync() => UnitOfWork.DriverLicensePhotos.CountElementAsync();

        private async void ValidateConnected(IAppActionResult result, Guid id, IFormFile file)
        {
            if (!await UnitOfWork.DriverLicenses.IsIdExistAsync(id))
                result.ErrorMessages.Add(Localizer["DriverLicenseNotFound"]);
            IValidatorOfUploadFile<Image> validatorFile = new ValidatorPhotoFile(UnitOfWork, Localizer);
            result.AddErrors(validatorFile.ValidateFile(file));
            result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
        }
    }
}
