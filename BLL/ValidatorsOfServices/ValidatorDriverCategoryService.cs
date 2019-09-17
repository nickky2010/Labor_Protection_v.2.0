using BLL.DTO.DriverCategories;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverCategoryService : AbstractValidatorOfServices<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory>
    {
        public override string EntityAlreadyExist { get => "DriverCategoryAlreadyExist"; }
        public override string EntityNotFound { get => "DriverCategoryNotFound"; }
        public override string EntitiesNotFound { get => "DriverCategorysNotFound"; }

        public ValidatorDriverCategoryService(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }
    }
}
