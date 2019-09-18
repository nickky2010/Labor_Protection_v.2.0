using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfServices
{
    internal abstract class AbstractValidatorOfServices<TGetDTO, TAddDTO, TUpdateDTO, TData> :
        IValidatorService<TGetDTO, TAddDTO, TUpdateDTO, TData>

        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        public IUnitOfWork<LaborProtectionContext> UnitOfWork { get; set; }
        public IAppActionResult<TGetDTO> GetResult { get; set; }
        public IAppActionResult<List<TGetDTO>> GetListResult { get; set; }
        public IAppActionResult DeleteResult { get; set; }

        protected abstract string EntityAlreadyExist { get; }
        protected abstract string EntityNotFound { get; }
        protected abstract string EntitiesNotFound { get; }


        public AbstractValidatorOfServices(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            GetResult = new AppActionResult<TGetDTO>();
            DeleteResult = new AppActionResult();
            GetListResult = new AppActionResult<List<TGetDTO>>();
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<IAppActionResult<TGetDTO>> ValidateAdd(TData data, TAddDTO model, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer)
        {
            if (data != null)
                GetResult.ErrorMessages.Add(localizer[EntityAlreadyExist]);
            else
                GetResult = await ValidateConnectedAddEntities(data, model, localizer);
            if (GetResult.ErrorMessages.Count!=0)
                GetResult.Status = (int)statusCodeIsError;
            else
                GetResult.Status = (int)statusCodeIsSuccess;
            return GetResult;
        }

        public virtual IAppActionResult<TGetDTO> ValidateDataFromDb(TData data, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer)
        {
            if (data == null)
                GetResult.ErrorMessages.Add(localizer[EntityNotFound]);
            if (GetResult.ErrorMessages.Count != 0)
                GetResult.Status = (int)statusCodeIsError;
            else
                GetResult.Status = (int)statusCodeIsSuccess;
            return GetResult;
        }
        public virtual IAppActionResult<TGetDTO> ValidateDataFromDbForUpdate(TData data, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer)
        {
            if (data == null)
                GetResult.ErrorMessages.Add(localizer[EntityNotFound]);
            if (GetResult.ErrorMessages.Count != 0)
                GetResult.Status = (int)statusCodeIsError;
            else
                GetResult.Status = (int)statusCodeIsSuccess;
            return GetResult;
        }

        public virtual IAppActionResult<List<TGetDTO>> ValidateDataFromDb(IList<TData> data, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer)
        {
            if (data == null)
                GetListResult.ErrorMessages.Add(localizer[EntitiesNotFound]);
            if (GetListResult.ErrorMessages.Count != 0)
                GetListResult.Status = (int)statusCodeIsError;
            else
                GetListResult.Status = (int)statusCodeIsSuccess;
            return GetListResult;
        }

        public virtual IAppActionResult ValidateDeleteDataFromDb(TData data, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer)
        {
            if (data == null)
                DeleteResult.ErrorMessages.Add(localizer[EntityNotFound]);
            if (DeleteResult.ErrorMessages.Count != 0)
                DeleteResult.Status = (int)statusCodeIsError;
            else
                DeleteResult.Status = (int)statusCodeIsSuccess;
            return DeleteResult;
        }

        public virtual async Task<IAppActionResult<TGetDTO>> ValidateUpdate(TData data, TUpdateDTO model, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer)
        {
            if (data == null)
                GetResult.ErrorMessages.Add(localizer[EntityNotFound]);
            else
                GetResult = await ValidateConnectedUpdateEntities(data, model, localizer);
            if (GetResult.ErrorMessages.Count != 0)
                GetResult.Status = (int)statusCodeIsError;
            else
                GetResult.Status = (int)statusCodeIsSuccess;
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
