using BLL.DTO.DriverCategories;
using BLL.ValidatorsOfServices.Abstract;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorDriverCategoryService : 
        AbstractValidatorOfCRUDDataBaseServices<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory>
    {
        protected override string EntityAlreadyExist { get => "DriverCategoryAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverCategoryNotFound"; }
        protected override string EntitiesNotFound { get => "DriverCategorysNotFound"; }

        public ValidatorDriverCategoryService(IUnitOfWork<LaborProtectionContext> unitOfWork)
            : base(unitOfWork) { }
    }
}
