using AutoMapper;
using BLL.Interfaces;
using BLL;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Web.Controllers.Abstract
{
    public abstract class AbstractBaseController<TGetDTO> : BaseController
        where TGetDTO : IGetDTO
    {
        public AbstractBaseController(IStringLocalizer<BLL.SharedResource> localizer, IMapper mapper)
            : base(localizer, mapper) { }

        protected IAppActionResult<List<TGetDTO>> SendResult(IAppActionResult<List<TGetDTO>> result)
        {
            SetResult(result.Status);
            return result;
        }

        protected IAppActionResult<TGetDTO> SendGetResult(IAppActionResult<TGetDTO> result)
        {
            SetResult(result.Status);
            return result;
        }
    }
}
