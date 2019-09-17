using AutoMapper;
using BLL;
using BLL.DTO.Positions;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Interfaces;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class PositionsController : AbstractController<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>,
        IControllerServices<PositionsController, IDataBaseService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>>
    {
        public PositionsController(IStringLocalizer<SharedResource> localizer, IMapper mapper, IDataBaseService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO> service)
            : base(localizer, mapper, service)
        {
            Validator = new ValidatorPositionController(Localizer);
        }
    }
}
