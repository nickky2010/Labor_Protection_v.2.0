using AutoMapper;
using BLL;
using BLL.DTO.Employees;
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
    public class ServiceEmployeeTest :
        AbstractCRUDServiceTest<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO, Employee>
    {
        protected override ICRUDDataBaseService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO> CreateService
            (IUnitOfWorkService unitOfWorkService, IMapper mapper, IStringLocalizer<SharedResource> localizer, IUnitOfWorkValidator unitOfWorkValidator)
        {
            var service = new EmployeeService(unitOfWorkService, mapper, unitOfWorkValidator);
            service.Localizer = localizer;
            return service;
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task>> SetupAddExpression(Employee data)
        {
            return a => a.Employees.AddAsync(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.Employees.CountElementAsync();
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupDeleteExpression(Employee data)
        {
            return a => a.Employees.Delete(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<Employee>>> SetupFindExpression()
        {
            return a => a.Employees.FindAsync(It.IsAny<Expression<Func<Employee, bool>>>());
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupUpdateExpression(Employee data)
        {
            return a => a.Employees.Update(data);
        }

        protected override Expression<Func<IUnitOfWorkValidator, IValidatorDTO<EmployeeAddDTO, EmployeeUpdateDTO, Employee>>> SetupValidatorExpression()
        {
            return a => a.ValidatorEmployeeDTO;
        }
    }
}
