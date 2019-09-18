using AutoMapper;
using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using Web.Interfaces;

namespace Web.Controllers
{
    public abstract class AbstractBaseController<TGetDTO, TAddDTO, TUpdateDTO> : Controller 

        where TGetDTO : IGetDTO
        where TUpdateDTO : IUpdateDTO
        where TAddDTO : IAddDTO
    {
        public IStringLocalizer<SharedResource> Localizer { get; protected set; }
        public IMapper Mapper { get; protected set; }
        public IDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> Service { get; protected set; }
        public IValidatorController<TGetDTO, TAddDTO, TUpdateDTO> Validator { get; protected set; }

        public AbstractBaseController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            IDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> service, IHostingEnvironment environment)
        {
            Service = service;
            Localizer = localizer;
            Service.Localizer = Localizer;
            Mapper = mapper;
            Service.Environment = environment;
        }

        protected void SetResult(int status)
        {
            ControllerContext.HttpContext.Response.StatusCode = status;
        }

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
        protected IAppActionResult SendResult(IAppActionResult result)
        {
            SetResult(result.Status);
            return result;
        }
    }
}
