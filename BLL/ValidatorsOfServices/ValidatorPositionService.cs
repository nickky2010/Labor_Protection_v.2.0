using BLL.DTO.Positions;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorPositionService: AbstractValidatorOfServices<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO, Position>
    {
        public override string EntityAlreadyExist { get => "PositionAlreadyExist"; }
        public override string EntityNotFound { get => "PositionNotFound"; }
        public override string EntitiesNotFound { get => "PositionsNotFound"; }

        public ValidatorPositionService(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            :base(unitOfWork, localizer) { }
    }
}
