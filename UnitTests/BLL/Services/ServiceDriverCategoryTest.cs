using AutoMapper;
using BLL;
using BLL.DTO.DriverCategories;
using BLL.Interfaces;
using BLL.Services;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitTests.BLL.Services.AbstractServicesTest;

namespace UnitTests.BLL.Services
{
    public class ServiceDriverCategoryTest :
        AbstractCRUDServiceTest<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory>
    {
        protected override ICRUDDataBaseService<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO> CreateService
            (IUnitOfWorkService unitOfWorkService, IMapper mapper, IStringLocalizer<SharedResource> localizer, IUnitOfWorkValidator unitOfWorkValidator)
        {
            var service = new DriverCategoryService(unitOfWorkService, mapper, unitOfWorkValidator);
            service.Localizer = localizer;
            return service;
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task>> SetupAddExpression(DriverCategory data)
        {
            return a => a.DriverCategories.AddAsync(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.DriverCategories.CountElementAsync();
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupDeleteExpression(DriverCategory data)
        {
            return a => a.DriverCategories.Delete(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<DriverCategory>>> SetupFindExpression()
        {
            return a => a.DriverCategories.FindAsync(It.IsAny<Expression<Func<DriverCategory, bool>>>());
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupUpdateExpression(DriverCategory data)
        {
            return a => a.DriverCategories.Update(data);
        }

        protected override Expression<Func<IUnitOfWorkValidator, IValidatorDTO<DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory>>> SetupValidatorExpression()
        {
            return a => a.ValidatorDriverCategoryDTO;
        }
    }
}
