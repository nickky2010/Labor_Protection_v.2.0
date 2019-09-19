using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfServices.Abstract
{
    internal abstract class AbstractValidatorOfCRUDDataBaseServices<TGetDTO, TAddDTO, TUpdateDTO, TData> :
        AbstractBaseValidatorOfServices,
        IValidatorCRUDDataBaseService<TGetDTO, TAddDTO, TUpdateDTO, TData>

        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        public IAppActionResult<TGetDTO> GetResult { get; set; }
        public IAppActionResult<List<TGetDTO>> GetListResult { get; set; }

        protected abstract string EntityAlreadyExist { get; }
        protected abstract string EntityNotFound { get; }
        protected abstract string EntitiesNotFound { get; }


        public AbstractValidatorOfCRUDDataBaseServices(IUnitOfWork<LaborProtectionContext> unitOfWork): base(unitOfWork)
        {
            GetResult = new AppActionResult<TGetDTO>();
            GetListResult = new AppActionResult<List<TGetDTO>>();
        }

        public virtual async Task<IAppActionResult<TGetDTO>> ValidateAdd(TData data, TAddDTO model, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer)
        {
            if (data != null)
                GetResult.ErrorMessages.Add(localizer[EntityAlreadyExist]);
            else
                GetResult = await ValidateConnectedAddEntities(data, model, localizer);
            SetStatus(GetResult, statusCodeIsError, statusCodeIsSuccess);
            return GetResult;
        }

        public virtual IAppActionResult<TGetDTO> ValidateDataFromDb(TData data, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer)
        {
            if (data == null)
                GetResult.ErrorMessages.Add(localizer[EntityNotFound]);
            SetStatus(GetResult, statusCodeIsError, statusCodeIsSuccess);
            return GetResult;
        }

        public virtual IAppActionResult<List<TGetDTO>> ValidateDataFromDb(IList<TData> data, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer)
        {
            if (data == null)
                GetListResult.ErrorMessages.Add(localizer[EntitiesNotFound]);
            SetStatus(GetListResult, statusCodeIsError, statusCodeIsSuccess);
            return GetListResult;
        }

        public virtual IAppActionResult ValidateDeleteDataFromDb(TData data, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer)
        {
            if (data == null)
                Result.ErrorMessages.Add(localizer[EntityNotFound]);
            SetStatus(Result, statusCodeIsError, statusCodeIsSuccess);
            return Result;
        }

        public virtual async Task<IAppActionResult<TGetDTO>> ValidateUpdate(TData data, TUpdateDTO model, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer)
        {
            if (data == null)
                GetResult.ErrorMessages.Add(localizer[EntityNotFound]);
            else
                GetResult = await ValidateConnectedUpdateEntities(data, model, localizer);
            SetStatus(GetResult, statusCodeIsError, statusCodeIsSuccess);
            return GetResult;
        }

        protected virtual Task<IAppActionResult<TGetDTO>> ValidateConnectedAddEntities(TData data, TAddDTO model, IStringLocalizer<SharedResource> localizer)
        {
            return Task.Run(() => GetResult);
        }
        protected virtual async Task<IAppActionResult<TGetDTO>> ValidateConnectedUpdateEntities(TData data, TUpdateDTO model, IStringLocalizer<SharedResource> localizer)
        {
            return await Task.Run(() => GetResult);
        }
    }
}
