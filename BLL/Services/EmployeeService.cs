﻿using AutoMapper;
using BLL.DTO.Employees;
using BLL.Interfaces;
using BLL.Services.Abstract;
using BLL.ValidatorsOfDTO;
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class EmployeeService :
        AbstractCRUDDataBaseService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO, Employee>
    {
        public EmployeeService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorEmployeeDTO(unitOfWorkService.UnitOfWorkLaborProtectionContext, Localizer);
        }

        protected override void AddDataToDbAsync(Employee data) => UnitOfWork.Employees.AddAsync(data);
        protected override void UpdateDataInDbAsync(Employee data) => UnitOfWork.Employees.Update(data);
        protected override void DeleteDataFromDbAsync(Employee data) => UnitOfWork.Employees.Delete(data);
        protected override Task<Employee> FindDataAsync(Guid id) => UnitOfWork.Employees.FindAsync(x => x.Id == id);
    }
}
