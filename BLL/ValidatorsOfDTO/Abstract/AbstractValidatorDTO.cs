using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfDTO.Abstract
{
    internal abstract class AbstractValidatorDTO<TGetDTO, TAddDTO, TUpdateDTO, TData> :
        AbstractBaseValidator,
        IValidatorDTO<TGetDTO, TAddDTO, TUpdateDTO, TData>

        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        public IAppActionResult<TData> DataResult { get; set; }
        public IAppActionResult<List<TData>> DataListResult { get; set; }
        protected abstract Task<TData> FindDataAsync(Guid id);
        protected abstract Task<List<TData>> FindPageDataAsync(int startItem, int countItem);
        protected abstract Task<TData> FindDataIfAddAsync(TAddDTO modelDTO);

        protected abstract string EntityAlreadyExist { get; }
        protected abstract string EntityNotFound { get; }
        protected abstract string EntitiesNotFound { get; }

        public AbstractValidatorDTO(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer) 
            : base(unitOfWork, localizer)
        {
            DataResult = new AppActionResult<TData>();
            DataListResult = new AppActionResult<List<TData>>();
        }
        public virtual async Task<IAppActionResult> ValidateAdd(TAddDTO model)
        {
            DataResult.Data = await FindDataIfAddAsync(model);
            if (DataResult.Data != null)
                DataResult.ErrorMessages.Add(Localizer[EntityAlreadyExist]);
            else
                DataResult = await ValidateConnectedEntities(DataResult.Data, model);
            SetStatus(DataResult, HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return DataResult;
        }

        public virtual async Task<IAppActionResult<TData>> ValidateGetData(Guid id)
        {
            DataResult.Data = await FindDataAsync(id);
            if (DataResult.Data == null)
                DataResult.ErrorMessages.Add(Localizer[EntityNotFound]);
            SetStatus(DataResult, HttpStatusCode.NotFound, HttpStatusCode.OK);
            return DataResult;
        }

        public virtual async Task<IAppActionResult<List<TData>>> ValidateGetData(int startItem, int countItem)
        {
            DataListResult.Data = await FindPageDataAsync(startItem, countItem);
            if (DataListResult.Data == null)
                DataListResult.ErrorMessages.Add(Localizer[EntitiesNotFound]);
            SetStatus(DataListResult, HttpStatusCode.NotFound, HttpStatusCode.OK);
            return DataListResult;
        }

        public virtual async Task<IAppActionResult<TData>> ValidateUpdate(TUpdateDTO model)
        {
            DataResult.Data = await FindDataAsync(model.Id);
            if (DataResult.Data == null)
                DataResult.ErrorMessages.Add(Localizer[EntityNotFound]);
            else
                DataResult = await ValidateConnectedEntities(DataResult.Data, model);
            SetStatus(DataResult, HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return DataResult;
        }

        public virtual async Task<IAppActionResult<TData>> ValidateDelete(Guid id)
        {
            DataResult.Data = await FindDataAsync(id);
            if (DataResult.Data == null)
                DataResult.ErrorMessages.Add(Localizer[EntityNotFound]);
            SetStatus(DataResult, HttpStatusCode.NotFound, HttpStatusCode.OK);
            return DataResult;
        }

        protected virtual async Task<IAppActionResult<TData>> ValidateConnectedEntities(TData data, TUpdateDTO model)
        {
            return await Task.Run(() => DataResult);
        }
        protected virtual async Task<IAppActionResult<TData>> ValidateConnectedEntities(TData data, TAddDTO model)
        {
            return await Task.Run(() => DataResult);
        }
    }
}
