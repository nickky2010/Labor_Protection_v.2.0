using BLL.DTO.Positions;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorPositionController : 
        AbstractValidatorOfControllers<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>
    {
        public ValidatorPositionController(IStringLocalizer<SharedResource> localizer):base(localizer) { }
    }
}
