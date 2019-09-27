using BLL;
using BLL.DTO.DriverCategories;
using BLL.Interfaces;
using BLL.ValidatorsOfDTO;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitTests.BLL.ValidatorsOfDTO
{
    public class ValidatorDriverCategoryDTOTest :
        AbstractCRUDValidatorDTOTest<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory>
    {
        protected override IValidatorDTO<DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory> CreateValidator
            (IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            return new ValidatorDriverCategoryDTO(unitOfWork, localizer);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.DriverCategories.CountElementAsync();
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<DriverCategory>>> SetupFindExpression()
        {
            return a => a.DriverCategories.FindAsync(It.IsAny<Expression<Func<DriverCategory, bool>>>());
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<List<DriverCategory>>>> SetupGetPageExpression()
        {
            return a => a.DriverCategories.GetPageAsync(It.IsAny<int>(), It.IsAny<int>());
        }
    }
}
