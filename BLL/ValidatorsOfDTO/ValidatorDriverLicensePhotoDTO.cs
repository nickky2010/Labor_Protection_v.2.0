using BLL.DTO.DriverLicensePhotos;
using BLL.Infrastructure.Extentions;
using BLL.Interfaces;
using BLL.ValidatorsOfDTO.Abstract;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorDriverLicensePhotoDTO :
        AbstractCRUDValidatorDTO<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto>
    {
        protected override string EntityAlreadyExist { get => "DriverLicensePhotoAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverLicensePhotoNotFound"; }
        protected override string EntitiesNotFound { get => "DriverLicensePhotosNotFound"; }

        public ValidatorDriverLicensePhotoDTO(IUnitOfWork<LaborProtectionContext> unitOfWork)
            : base(unitOfWork) { }

        public override async Task<IAppActionResult> ValidateAdd(DriverLicensePhotoAddDTO model)
        {
            var result = await base.ValidateAdd(model);
            if (result.IsSuccess)
                ValidateConnected(result, model.DriverLicenseId, model.Picture);
            return result;
        }

        public override async Task<IAppActionResult<DriverLicensePhoto>> ValidateUpdate(DriverLicensePhotoUpdateDTO model)
        {
            var result = await base.ValidateUpdate(model);
            if (result.IsSuccess)
                ValidateConnected(result, model.DriverLicenseId, model.Picture);
            if (!result.IsSuccess)
                result.Data = default;
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
            IValidatorOfUploadFile<Image> validatorFile = new ValidatorPhotoFile();
            validatorFile.Localizer = Localizer;
            result.AddErrors(validatorFile.ValidateFile(file));
            result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
        }
    }
}
