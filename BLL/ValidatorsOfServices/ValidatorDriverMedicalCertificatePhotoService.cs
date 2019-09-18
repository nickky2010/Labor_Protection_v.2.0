using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverMedicalCertificatePhotoService : 
        AbstractValidatorOfServices<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto>
    {
        protected override string EntityAlreadyExist { get => "DriverMedicalCertificatePhotoAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverMedicalCertificatePhotoNotFound"; }
        protected override string EntitiesNotFound { get => "DriverMedicalCertificatePhotosNotFound"; }

        public ValidatorDriverMedicalCertificatePhotoService(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            :base(unitOfWork, localizer) { }

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
