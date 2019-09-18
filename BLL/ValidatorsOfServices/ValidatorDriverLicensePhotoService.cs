using BLL.DTO.DriverLicensePhotos;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverLicensePhotoService: 
        AbstractValidatorOfServices<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto>
    {
        protected override string EntityAlreadyExist { get => "DriverLicensePhotoAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverLicensePhotoNotFound"; }
        protected override string EntitiesNotFound { get => "DriverLicensePhotosNotFound"; }

        public ValidatorDriverLicensePhotoService(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            :base(unitOfWork, localizer) { }

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
