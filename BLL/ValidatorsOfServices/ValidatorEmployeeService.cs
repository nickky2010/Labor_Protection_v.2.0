using BLL.DTO.Employees;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorEmployeeService: AbstractValidatorOfServices<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO, Employee>
    {
        public override string EntityAlreadyExist { get => "EmployeeAlreadyExist"; }
        public override string EntityNotFound { get => "EmployeeNotFound"; }
        public override string EntitiesNotFound { get => "EmployeesNotFound"; }

        public ValidatorEmployeeService(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            :base(unitOfWork, localizer) { }

        protected override async Task<IAppActionResult<EmployeeGetDTO>> ValidateConnectedEntities(IAppActionResult<EmployeeGetDTO> getResult, Employee data, EmployeeAddDTO model)
        {
            if (!await UnitOfWork.Positions.IsIdExistAsync(model.PositionId))
                getResult.ErrorMessages.Add(Localizer["PositionNotFound"]);
            return getResult;
        }
        protected override async Task<IAppActionResult<EmployeeUpdateDTO>> ValidateConnectedEntities(IAppActionResult<EmployeeUpdateDTO> updateResult, Employee data, EmployeeUpdateDTO model)
        {
            if (!await UnitOfWork.Positions.IsIdExistAsync(model.PositionId))
                updateResult.ErrorMessages.Add(Localizer["PositionNotFound"]);
            return updateResult;
        }
    }
}
