using BLL.DTO.DriverCategories;
using BLL.ValidatorsOfDTO.Abstract;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorDriverCategoryDTO :
        AbstractCRUDValidatorDTO<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory>
    {
        protected override string EntityAlreadyExist { get => "DriverCategoryAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverCategoryNotFound"; }
        protected override string EntitiesNotFound { get => "DriverCategorysNotFound"; }

        public ValidatorDriverCategoryDTO(IUnitOfWork<LaborProtectionContext> unitOfWork)
            : base(unitOfWork) { }

        protected override Task<DriverCategory> FindDataAsync(Guid id) =>
            UnitOfWork.DriverCategories.FindAsync(x => x.Id == id);
        protected override Task<List<DriverCategory>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.DriverCategories.GetPageAsync(startItem, countItem);
        protected override Task<DriverCategory> FindDataAsync(DriverCategoryAddDTO modelDTO) =>
            UnitOfWork.DriverCategories.FindAsync(x => x.Name == modelDTO.Name);
        protected override Task<int> GetCountElementAsync() => UnitOfWork.DriverCategories.CountElementAsync();
    }
}
