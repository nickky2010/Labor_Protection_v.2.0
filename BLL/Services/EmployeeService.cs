using AutoMapper;
using BLL.DTO.Employees;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class EmployeeService : 
        AbstractService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO, Employee>         
    {
        public EmployeeService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorEmployeeService(unitOfWorkService.UnitOfWorkLaborProtectionContext, Localizer);
        }

        protected override void AddDataToDbAsync(Employee data) => UnitOfWork.Employees.AddAsync(data);
        protected override void UpdateDataInDbAsync(Employee data) => UnitOfWork.Employees.Update(data);
        protected override void DeleteDataFromDbAsync(Employee data) => UnitOfWork.Employees.Delete(data);
        protected override Task<Employee> FindDataAsync(Guid id) => UnitOfWork.Employees.FindAsync(x => x.Id == id);

        protected override Task<Employee> FindDataIfAddAsync(EmployeeAddDTO modelDTO)
        {
            return UnitOfWork.Employees.FindAsync(x => x.FirstName == modelDTO.FirstName &&
                x.Surname == modelDTO.Surname && x.Patronymic == modelDTO.Patronymic && x.Position.Id == modelDTO.PositionId);
        }

        protected override async Task<List<Employee>> FindPageDataAsync(int startItem, int countItem)
        {
            return await UnitOfWork.Employees.GetPageAsync(startItem, countItem);
        }

        protected override Task<Employee> FindDataIfUpdateAsync(EmployeeUpdateDTO modelDTO)
        {
            return UnitOfWork.Employees.FindAsync(x => x.Id == modelDTO.Id);
        }
    }
}
