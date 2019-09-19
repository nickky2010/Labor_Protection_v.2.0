using AutoMapper;
using BLL.DTO.Positions;
using BLL.Interfaces;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Interfaces;
using Web.ValidatorsOfControllers;
using Web.Controllers.Abstract;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class PositionsController : AbstractCRUDController<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>,
        IControllerServices<PositionsController, ICRUDDataBaseService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>>
    {
        public PositionsController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            ICRUDDataBaseService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorPositionController(Localizer);
        }
    }
}
