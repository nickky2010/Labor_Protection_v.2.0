using BLL.DTO.DriverCategories;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverCategoryService : 
        AbstractValidatorOfServices<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory>
    {
        protected override string EntityAlreadyExist { get => "DriverCategoryAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverCategoryNotFound"; }
        protected override string EntitiesNotFound { get => "DriverCategorysNotFound"; }

        public ValidatorDriverCategoryService(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }
    }
}
