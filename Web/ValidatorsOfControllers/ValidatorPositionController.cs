﻿using BLL;
using BLL.DTO.Positions;
using Microsoft.Extensions.Localization;
using Web.ValidatorsOfControllers.Abstract;

namespace Web.ValidatorsOfControllers
{
    internal class ValidatorPositionController :
        AbstractValidatorOfControllers<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>
    {
        public ValidatorPositionController(IStringLocalizer<SharedResource> localizer) : base(localizer) { }
    }
}
