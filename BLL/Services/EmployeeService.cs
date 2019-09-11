﻿using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeService : Service, IDataBaseService<EmployeeDTO>
    {
        public IUnitOfWork<LaborProtectionContext> UnitOfWork { get; set; }
        public IMapper Mapper { get; set; }
        public IStringLocalizer<SharedResource> Localizer { get; set; }

        public EmployeeService(IUnitOfWorkService unitOfWorkService, IMapper mapper)
        {
            UnitOfWork = unitOfWorkService.UnitOfWorkLaborProtectionContext;
            Mapper = mapper;
        }

        public async Task<IAppActionResult> AddAsync(EmployeeDTO entity)
        {
            var employee = await UnitOfWork.Employees.FindAsync(x => x.FirstName == entity.FirstName &&
                x.Surname == entity.Surname && x.Patronymic == entity.Patronymic && x.Position.Name == entity.Position.Name);
            if (employee == null)
            {
                UnitOfWork.Employees.AddAsync(Mapper.Map<EmployeeDTO, Employee>(entity));
                await UnitOfWork.SaveChangesAsync();
                employee = await UnitOfWork.Employees.FindAsync(x => x.FirstName == entity.FirstName &&
                    x.Surname == entity.Surname && x.Patronymic == entity.Patronymic && x.Position.Name == entity.Position.Name);
                if (employee != null)
                {
                    return new AppActionResult<EmployeeDTO>
                    {
                        Data = Mapper.Map<Employee, EmployeeDTO>(employee),
                        Status = (int)HttpStatusCode.Created
                    };
                }
                return new AppActionResult { Status = (int)HttpStatusCode.InternalServerError, ErrorMessages = new List<string> { Localizer["AfterAddEmployeeNotFound"] } };
            }
            return new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["EmployeeAlreadyExist"] } };
        }

        public async Task<IAppActionResult> DeleteAsync(Guid guid)
        {
            var employee = await UnitOfWork.Employees.FindAsync(x => x.Id == guid);
            if (employee != null)
            {
                UnitOfWork.Employees.Delete(employee);
                await UnitOfWork.SaveChangesAsync();
                employee = await UnitOfWork.Employees.FindAsync(x => x.Id == guid);
                if (employee == null)
                {
                    return new AppActionResult
                    {
                        Status = (int)HttpStatusCode.OK
                    };
                }
                return new AppActionResult { Status = (int)HttpStatusCode.InternalServerError, ErrorMessages = new List<string> { Localizer["EmployeeFoundAfterDelete"] } };
            }
            return new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["EmployeeNotFound"] } };
        }

        public async Task<IAppActionResult> GetAsync(Guid guid)
        {
            var employee = await UnitOfWork.Employees.FindAsync(x => x.Id == guid);
            if (employee != null)
            {
                return new AppActionResult<EmployeeDTO>
                {
                    Data = Mapper.Map<Employee, EmployeeDTO>(employee),
                    Status = (int)HttpStatusCode.OK
                };
            }
            return new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["EmployeeNotFound"] } };
        }

        public async Task<IAppActionResult> GetPageAsync(int startItem, int countItem)
        {
            var employees = await UnitOfWork.Employees.GetPageAsync(startItem, countItem);
            if (employees != null && employees.Count != 0)
            {
                return new AppActionResult<ICollection<EmployeeDTO>>
                {
                    Data = Mapper.Map<IList<Employee>, List<EmployeeDTO>>(employees),
                    Status = (int)HttpStatusCode.OK
                };
            }
            return new AppActionResult { Status = (int)HttpStatusCode.NotFound, ErrorMessages = new List<string> { Localizer["EmployeesNotFound"] } };
        }

        public async Task<IAppActionResult> UpdateAsync(EmployeeDTO model)
        {
            var data = await UnitOfWork.Employees.FindAsync(i => i.Id == model.Id);
            if (data != null)
            {
                Mapper.Map(model, data);
                UnitOfWork.Employees.Update(data);
                await UnitOfWork.SaveChangesAsync();
                return await GetAsync(model.Id);
            }
            return new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["EmployeeNotFound"] } };
        }
    }
}