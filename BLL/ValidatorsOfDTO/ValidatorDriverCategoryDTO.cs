using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO.DriverCategories;
using BLL.ValidatorsOfDTO.Abstract;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorDriverCategoryDTO : 
        AbstractValidatorDTO<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory>
    {
        protected override string EntityAlreadyExist { get => "DriverCategoryAlreadyExist"; }
        protected override string EntityNotFound { get => "DriverCategoryNotFound"; }
        protected override string EntitiesNotFound { get => "DriverCategorysNotFound"; }

        public ValidatorDriverCategoryDTO(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }

        protected override Task<DriverCategory> FindDataAsync(Guid id) => 
            UnitOfWork.DriverCategories.FindAsync(x => x.Id == id);
        protected override Task<List<DriverCategory>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.DriverCategories.GetPageAsync(startItem, countItem);
        protected override Task<DriverCategory> FindDataIfAddAsync(DriverCategoryAddDTO modelDTO) =>
            UnitOfWork.DriverCategories.FindAsync(x=>x.Name == modelDTO.Name);
    }
}
