using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Net;

namespace BLL.Services
{
    internal abstract class AbstractService<TGetDTO, TAddDTO, TUpdateDTO> : IService<LaborProtectionContext>
        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
    {
        public IStringLocalizer<SharedResource> Localizer { get; set; }
        public IUnitOfWork<LaborProtectionContext> UnitOfWork { get; protected set; }
        public IMapper Mapper { get; protected set; }

        public AbstractService(IUnitOfWorkService unitOfWorkService, IMapper mapper)
        {
            UnitOfWork = unitOfWorkService.UnitOfWorkLaborProtectionContext;
            Mapper = mapper;
        }
        protected virtual IAppActionResult SendError(HttpStatusCode statusCode, string lokalizerKey)
        {
            return new AppActionResult { Status = (int)statusCode, ErrorMessages = new List<string> { Localizer[lokalizerKey] } };
        }

        protected virtual IAppActionResult<TGetDTO> SendGetError(HttpStatusCode statusCode, string lokalizerKey)
        {
            return new AppActionResult<TGetDTO> { Status = (int)statusCode, ErrorMessages = new List<string> { Localizer[lokalizerKey] } };
        }
        protected virtual IAppActionResult<IList<TGetDTO>> SendForListGetError(HttpStatusCode statusCode, string lokalizerKey)
        {
            return new AppActionResult<IList<TGetDTO>> { Status = (int)statusCode, ErrorMessages = new List<string> { Localizer[lokalizerKey] } };
        }
        protected virtual IAppActionResult<TUpdateDTO> SendUpdateError(HttpStatusCode statusCode, string lokalizerKey)
        {
            return new AppActionResult<TUpdateDTO> { Status = (int)statusCode, ErrorMessages = new List<string> { Localizer[lokalizerKey] } };
        }


        protected virtual IAppActionResult SendResult(HttpStatusCode statusCode)
        {
            return new AppActionResult { Status = (int)statusCode};
        }

        protected virtual IAppActionResult<TGetDTO> SendData(TGetDTO tDTO, HttpStatusCode statusCode)
        {
            return new AppActionResult<TGetDTO> { Data = tDTO, Status = (int)statusCode };
        }
        protected virtual IAppActionResult<TAddDTO> SendAddData(TAddDTO tDTO, HttpStatusCode statusCode)
        {
            return new AppActionResult<TAddDTO> { Data = tDTO, Status = (int)statusCode };
        }
        protected virtual IAppActionResult<TUpdateDTO> SendUpdateData(TUpdateDTO tDTO, HttpStatusCode statusCode)
        {
            return new AppActionResult<TUpdateDTO> { Data = tDTO, Status = (int)statusCode };
        }

        protected virtual IAppActionResult<IList<TGetDTO>> SendListData(IList<TGetDTO> tDTOs, HttpStatusCode statusCode)
        {
            return new AppActionResult<IList<TGetDTO>> { Data = tDTOs, Status = (int)statusCode };
        }
    }
}
