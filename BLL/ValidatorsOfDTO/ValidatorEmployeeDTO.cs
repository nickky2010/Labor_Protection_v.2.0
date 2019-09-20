using BLL.DTO.Employees;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using BLL.ValidatorsOfDTO.Abstract;
using System;
using System.Collections.Generic;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorEmployeeDTO: 
        AbstractValidatorDTO<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO, Employee>
    {
        protected override string EntityAlreadyExist { get => "EmployeeAlreadyExist"; }
        protected override string EntityNotFound { get => "EmployeeNotFound"; }
        protected override string EntitiesNotFound { get => "EmployeesNotFound"; }

        public ValidatorEmployeeDTO(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }

        protected override async Task<IAppActionResult<Employee>> ValidateConnectedEntities(Employee data, EmployeeAddDTO model)
        {
            if (!await UnitOfWork.Positions.IsIdExistAsync(model.PositionId))
                DataResult.ErrorMessages.Add(Localizer["PositionNotFound"]);
            return DataResult;
        }
        protected override async Task<IAppActionResult<Employee>> ValidateConnectedEntities(Employee data, EmployeeUpdateDTO model)
        {
            if (!await UnitOfWork.Positions.IsIdExistAsync(model.PositionId))
                DataResult.ErrorMessages.Add(Localizer["PositionNotFound"]);
            return DataResult;
        }

        protected override Task<Employee> FindDataAsync(Guid id) =>
            UnitOfWork.Employees.FindAsync(x => x.Id == id);

        protected override Task<List<Employee>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.Employees.GetPageAsync(startItem, countItem);

        protected override Task<Employee> FindDataIfAddAsync(EmployeeAddDTO modelDTO) =>
            UnitOfWork.Employees.FindAsync(x => x.Surname == modelDTO.Surname && x.FirstName == modelDTO.FirstName && x.Patronymic == modelDTO.Patronymic);
    }
}
