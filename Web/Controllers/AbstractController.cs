using AutoMapper;
using BLL;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Web.Controllers
{
    public abstract class AbstractController<TGetDTO, TAddDTO, TUpdateDTO> : Controller
        where TGetDTO : IGetDTO
        where TUpdateDTO : IUpdateDTO
        where TAddDTO : IAddDTO
    {
        public IStringLocalizer<SharedResource> Localizer { get; protected set; }
        public IMapper Mapper { get; private set; }
        public IDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> Service { get; protected set; }

        public AbstractController(IStringLocalizer<SharedResource> localizer, IMapper mapper, IDataBaseService<TGetDTO, TAddDTO, TUpdateDTO> service)
        {
            Service = service;
            Localizer = localizer;
            Service.Localizer = Localizer;
            Mapper = mapper;
        }

        protected virtual void SetResult(int status)
        {
            ControllerContext.HttpContext.Response.StatusCode = status;
        }

        protected virtual IAppActionResult<IList<TGetDTO>> SendErrorForGetList(int statusCode, string lokalizerKey)
        {
            SetResult(statusCode);
            return new AppActionResult<IList<TGetDTO>> { Status = statusCode, ErrorMessages = new List<string> { Localizer[lokalizerKey] } };
        }
        protected virtual IAppActionResult<TGetDTO> SendErrorForGet(int statusCode, string lokalizerKey)
        {
            SetResult(statusCode);
            return new AppActionResult<TGetDTO> { Status = statusCode, ErrorMessages = new List<string> { Localizer[lokalizerKey] } };
        }
        protected virtual IAppActionResult<TUpdateDTO> SendErrorForUpdate(int statusCode, string lokalizerKey)
        {
            SetResult(statusCode);
            return new AppActionResult<TUpdateDTO> { Status = statusCode, ErrorMessages = new List<string> { Localizer[lokalizerKey] } };
        }

        protected virtual IAppActionResult<IList<TGetDTO>> SendResult(IAppActionResult<IList<TGetDTO>> result)
        {
            SetResult(result.Status);
            return result;
        }

        protected virtual IAppActionResult<TGetDTO> SendGetResult(IAppActionResult<TGetDTO> result)
        {
            SetResult(result.Status);
            return result;
        }
        protected virtual IAppActionResult<TUpdateDTO> SendUpdateResult(IAppActionResult<TUpdateDTO> result)
        {
            SetResult(result.Status);
            return result;
        }
        protected virtual IAppActionResult SendResult(IAppActionResult result)
        {
            SetResult(result.Status);
            return result;
        }
    }
}
