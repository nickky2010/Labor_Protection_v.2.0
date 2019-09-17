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
    internal abstract class AbstractValidatorOfServices<TGetDTO, TAddDTO, TUpdateDTO, TData> : IValidatorService<TGetDTO, TAddDTO, TUpdateDTO, TData>
        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        public IUnitOfWork<LaborProtectionContext> UnitOfWork { get; set; }
        public IStringLocalizer<SharedResource> Localizer { get; set; }
        public IAppActionResult<TGetDTO> GetResult { get; set; }
        public IAppActionResult<List<TGetDTO>> GetListResult { get; set; }
        public IAppActionResult<TUpdateDTO> UpdateResult { get; set; }
        public IAppActionResult DeleteResult { get; set; }

        public abstract string EntityAlreadyExist { get; }
        public abstract string EntityNotFound { get; }
        public abstract string EntitiesNotFound { get; }

        public AbstractValidatorOfServices(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            GetResult = new AppActionResult<TGetDTO>();
            UpdateResult = new AppActionResult<TUpdateDTO>();
            DeleteResult = new AppActionResult();
            GetListResult = new AppActionResult<List<TGetDTO>>();
            UnitOfWork = unitOfWork;
            Localizer = localizer;
        }

        public virtual async Task<IAppActionResult<TGetDTO>> ValidateAdd(TData data, TAddDTO model, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess)
        {
            if (data != null)
                GetResult.ErrorMessages.Add(Localizer[EntityAlreadyExist]);
            else
                GetResult = await ValidateConnectedEntities(GetResult, data, model);
            if (GetResult.ErrorMessages.Count!=0)
                GetResult.Status = (int)statusCodeIsError;
            else
                GetResult.Status = (int)statusCodeIsSuccess;
            return GetResult;
        }

        public virtual IAppActionResult<TGetDTO> ValidateDataFromDb(TData data, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess)
        {
            if (data == null)
                GetResult.ErrorMessages.Add(Localizer[EntityNotFound]);
            if (GetResult.ErrorMessages.Count != 0)
                GetResult.Status = (int)statusCodeIsError;
            else
                GetResult.Status = (int)statusCodeIsSuccess;
            return GetResult;
        }
        public virtual IAppActionResult<TUpdateDTO> ValidateDataFromDbForUpdate(TData data, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess)
        {
            if (data == null)
                UpdateResult.ErrorMessages.Add(Localizer[EntityNotFound]);
            if (UpdateResult.ErrorMessages.Count != 0)
                UpdateResult.Status = (int)statusCodeIsError;
            else
                UpdateResult.Status = (int)statusCodeIsSuccess;
            return UpdateResult;
        }

        public virtual IAppActionResult<List<TGetDTO>> ValidateDataFromDb(IList<TData> data, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess)
        {
            if (data == null)
                GetListResult.ErrorMessages.Add(Localizer[EntitiesNotFound]);
            if (GetListResult.ErrorMessages.Count != 0)
                GetListResult.Status = (int)statusCodeIsError;
            else
                GetListResult.Status = (int)statusCodeIsSuccess;
            return GetListResult;
        }

        public virtual IAppActionResult ValidateDeleteDataFromDb(TData data, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess)
        {
            if (data == null)
                DeleteResult.ErrorMessages.Add(Localizer[EntityNotFound]);
            if (DeleteResult.ErrorMessages.Count != 0)
                DeleteResult.Status = (int)statusCodeIsError;
            else
                DeleteResult.Status = (int)statusCodeIsSuccess;
            return DeleteResult;
        }

        public virtual async Task<IAppActionResult<TUpdateDTO>> ValidateUpdate(TData data, TUpdateDTO model, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess)
        {
            if (data == null)
                UpdateResult.ErrorMessages.Add(Localizer[EntityNotFound]);
            else
                UpdateResult = await ValidateConnectedEntities(UpdateResult, data, model);
            if (UpdateResult.ErrorMessages.Count != 0)
                UpdateResult.Status = (int)statusCodeIsError;
            else
                UpdateResult.Status = (int)statusCodeIsSuccess;
            return UpdateResult;
        }

        protected virtual async Task<IAppActionResult<TUpdateDTO>> ValidateConnectedEntities(IAppActionResult<TUpdateDTO> updateResult, TData data, TUpdateDTO model)
        {
            return await Task.Run(() => updateResult);
        }
        protected virtual async Task<IAppActionResult<TGetDTO>> ValidateConnectedEntities(IAppActionResult<TGetDTO> getResult, TData data, TAddDTO model)
        {
            return await Task.Run(() => getResult);
        }
    }
}
