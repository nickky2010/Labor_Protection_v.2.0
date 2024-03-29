﻿using AutoMapper;
using BLL;
using BLL.DTO.Positions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Controllers.Abstract;
using Web.ValidatorsOfControllers;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class PositionsController : AbstractCRUDDataController<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>
    {
        public PositionsController(IStringLocalizer<SharedResource> localizer, IMapper mapper,
            ICRUDDataBaseService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorPositionController(Localizer);
        }
    }
}
