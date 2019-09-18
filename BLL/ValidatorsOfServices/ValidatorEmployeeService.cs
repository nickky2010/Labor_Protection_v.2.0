using BLL.DTO.Employees;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorEmployeeService: 
        AbstractValidatorOfServices<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO, Employee>
    {
        protected override string EntityAlreadyExist { get => "EmployeeAlreadyExist"; }
        protected override string EntityNotFound { get => "EmployeeNotFound"; }
        protected override string EntitiesNotFound { get => "EmployeesNotFound"; }

        public ValidatorEmployeeService(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            :base(unitOfWork, localizer) { }

        protected override async Task<IAppActionResult<EmployeeGetDTO>> ValidateConnectedAddEntities(Employee data, 
            EmployeeAddDTO model, IStringLocalizer<SharedResource> localizer)
        {
            if (!await UnitOfWork.Positions.IsIdExistAsync(model.PositionId))
                GetResult.ErrorMessages.Add(localizer["PositionNotFound"]);
            return GetResult;
        }
        protected override async Task<IAppActionResult<EmployeeGetDTO>> ValidateConnectedUpdateEntities(Employee data, 
            EmployeeUpdateDTO model, IStringLocalizer<SharedResource> localizer)
        {
            if (!await UnitOfWork.Positions.IsIdExistAsync(model.PositionId))
                GetResult.ErrorMessages.Add(localizer["PositionNotFound"]);
            return GetResult;
        }
    }
}
