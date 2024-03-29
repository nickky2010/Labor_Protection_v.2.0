﻿using BLL.DTO.Employees;
using BLL.Infrastructure.Extentions;
using BLL.Interfaces;
using BLL.ValidatorsOfDTO.Abstract;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorEmployeeDTO :
        AbstractCRUDValidatorDTO<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO, Employee>
    {
        protected override string EntityAlreadyExist { get => "EmployeeAlreadyExist"; }
        protected override string EntityNotFound { get => "EmployeeNotFound"; }
        protected override string EntitiesNotFound { get => "EmployeesNotFound"; }

        public ValidatorEmployeeDTO(IUnitOfWork<LaborProtectionContext> unitOfWork)
            : base(unitOfWork) { }

        public override async Task<IAppActionResult> ValidateAdd(EmployeeAddDTO model)
        {
            var result = await base.ValidateAdd(model);
            if (result.IsSuccess)
                ValidateConnected(result, model.PositionId);
            return result;
        }

        public override async Task<IAppActionResult<Employee>> ValidateUpdate(EmployeeUpdateDTO model)
        {
            var result = await base.ValidateUpdate(model);
            if (result.IsSuccess)
                ValidateConnected(result, model.PositionId);
            if (!result.IsSuccess)
                result.Data = default;
            return result;
        }

        protected override Task<Employee> FindDataAsync(Guid id) =>
            UnitOfWork.Employees.FindAsync(x => x.Id == id);

        protected override Task<List<Employee>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.Employees.GetPageAsync(startItem, countItem);

        protected override Task<Employee> FindDataAsync(EmployeeAddDTO modelDTO) =>
            UnitOfWork.Employees.FindAsync(x => x.Surname == modelDTO.Surname && x.FirstName == modelDTO.FirstName && x.Patronymic == modelDTO.Patronymic);
        protected override Task<int> GetCountElementAsync() => UnitOfWork.Employees.CountElementAsync();

        private async void ValidateConnected(IAppActionResult result, Guid id)
        {
            if (!await UnitOfWork.Positions.IsIdExistAsync(id))
                result.ErrorMessages.Add(Localizer["PositionNotFound"]);
            result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
        }
    }
}
