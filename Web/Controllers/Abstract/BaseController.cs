using AutoMapper;
using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Web.Controllers.Abstract
{
    public class BaseController : Controller
    {
        public IStringLocalizer<SharedResource> Localizer { get; protected set; }
        public IMapper Mapper { get; protected set; }

        public BaseController(IStringLocalizer<SharedResource> localizer, IMapper mapper)
        {
            Localizer = localizer;
            Mapper = mapper;
        }

        protected void SetResponseStatusCode(IAppActionResult result)
        {
            ControllerContext.HttpContext.Response.StatusCode = result.Status;
        }
    }
}
