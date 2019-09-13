using AutoMapper;
using BLL.DTO.Employees;
using BLL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class EmployeeService : AbstractService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO>, 
        IDataBaseService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO>
    {
        public EmployeeService(IUnitOfWorkService unitOfWorkService, IMapper mapper) : base(unitOfWorkService, mapper) { }

        public async Task<IAppActionResult<EmployeeGetDTO>> AddAsync(EmployeeAddDTO modelDTO)
        {
            var employee = await UnitOfWork.Employees.FindAsync(x => x.FirstName == modelDTO.FirstName &&
                x.Surname == modelDTO.Surname && x.Patronymic == modelDTO.Patronymic && x.Position.Id == modelDTO.PositionId);
            if (employee != null)
                return SendGetError(HttpStatusCode.BadRequest, "EmployeeAlreadyExist");
            employee = Mapper.Map<Employee>(modelDTO);
            employee.Id = Guid.NewGuid();
            await UnitOfWork.Employees.AddAsync(employee);
            if(await UnitOfWork.SaveChangesAsync()==0)
                return SendGetError(HttpStatusCode.InternalServerError, "DataNotWrittenToDB");
            return SendData(Mapper.Map<Employee, EmployeeGetDTO>(employee), HttpStatusCode.Created);
        }

        public async Task<IAppActionResult> DeleteAsync(Guid guid)
        {
            var employee = await UnitOfWork.Employees.FindAsync(x => x.Id == guid);
            if (employee == null)
                return SendError(HttpStatusCode.BadRequest, "EmployeeNotFound");
            UnitOfWork.Employees.Delete(employee);
            if (await UnitOfWork.SaveChangesAsync() == 0)
                return SendError(HttpStatusCode.InternalServerError, "DataNotDeletedFromDB");
            return SendResult(HttpStatusCode.OK);
        }

        public async Task<IAppActionResult<EmployeeGetDTO>> GetAsync(Guid guid)
        {
            var employee = await UnitOfWork.Employees.FindAsync(x => x.Id == guid);
            if (employee == null)
                return SendGetError(HttpStatusCode.BadRequest, "EmployeeNotFound");
            return SendData(Mapper.Map<Employee, EmployeeGetDTO>(employee), HttpStatusCode.OK);
        }

        public async Task<IAppActionResult<IList<EmployeeGetDTO>>> GetPageAsync(int startItem, int countItem)
        {
            var data = await UnitOfWork.Employees.GetPageAsync(startItem, countItem);
            if (data == null)
                return SendForListGetError(HttpStatusCode.NotFound, "EmployeesNotFound");
            return SendListData(Mapper.Map<IList<Employee>, List<EmployeeGetDTO>>(data), HttpStatusCode.OK);
        }

        public async Task<IAppActionResult<EmployeeUpdateDTO>> UpdateAsync(EmployeeUpdateDTO model)
        {
            var data = await UnitOfWork.Employees.FindAsync(i => i.Id == model.Id);
            if (data == null)
                return SendUpdateError(HttpStatusCode.NotFound, "EmployeeNotFound");
            var position = await UnitOfWork.Positions.FindAsync(x => x.Id == data.Position.Id);
            if (position == null)
                return SendUpdateError(HttpStatusCode.BadRequest, "PositionNotFound");
            Mapper.Map(model, data);
            UnitOfWork.Employees.Update(data);
            if (await UnitOfWork.SaveChangesAsync() == 0)
                return SendUpdateError(HttpStatusCode.InternalServerError, "DataNotUpdatedInDB");
            return SendUpdateData(Mapper.Map<Employee, EmployeeUpdateDTO>(data), HttpStatusCode.OK);
        }
    }
}
