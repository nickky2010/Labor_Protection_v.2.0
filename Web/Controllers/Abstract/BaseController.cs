using AutoMapper;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Web.Controllers.Abstract
{
    public class BaseController : Controller 
    {
        public IStringLocalizer<BLL.SharedResource> Localizer { get; protected set; }
        public IMapper Mapper { get; protected set; }

        public BaseController(IStringLocalizer<BLL.SharedResource> localizer, IMapper mapper)
        {
            Localizer = localizer;
            Mapper = mapper;
        }

        protected void SetResult(int status)
        {
            ControllerContext.HttpContext.Response.StatusCode = status;
        }

        protected IAppActionResult SendResult(IAppActionResult result)
        {
            SetResult(result.Status);
            return result;
        }
    }
}
