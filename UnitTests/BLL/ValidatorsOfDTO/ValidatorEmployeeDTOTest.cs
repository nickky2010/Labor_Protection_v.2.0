using BLL;
using BLL.DTO.Employees;
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
    public class ValidatorEmployeeDTOTest :
        AbstractCRUDValidatorDTOWithConnectedEntitiesTest<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO, Employee>
    {
        protected override IValidatorDTO<EmployeeAddDTO, EmployeeUpdateDTO, Employee> CreateValidator
            (IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            var validator = new ValidatorEmployeeDTO(unitOfWork);
            validator.Localizer = localizer;
            return validator;
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.Employees.CountElementAsync();
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<Employee>>> SetupFindExpression()
        {
            return a => a.Employees.FindAsync(It.IsAny<Expression<Func<Employee, bool>>>());
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<List<Employee>>>> SetupGetPageExpression()
        {
            return a => a.Employees.GetPageAsync(It.IsAny<int>(), It.IsAny<int>());
        }

        protected override void SetMocksForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, EmployeeAddDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x => x.Positions.IsIdExistAsync(dataDTO.PositionId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }

        protected override void SetMocksForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, EmployeeUpdateDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x => x.Positions.IsIdExistAsync(dataDTO.PositionId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }
    }
}
