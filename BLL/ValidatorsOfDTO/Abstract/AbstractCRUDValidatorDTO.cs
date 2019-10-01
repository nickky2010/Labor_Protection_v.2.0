using BLL.Infrastructure;
using BLL.Infrastructure.Extentions;
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
    internal abstract class AbstractCRUDValidatorDTO<TGetDTO, TAddDTO, TUpdateDTO, TData> :
        IValidatorDTO<TAddDTO, TUpdateDTO, TData>

        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        protected abstract Task<int> GetCountElementAsync();
        protected abstract Task<TData> FindDataAsync(Guid id);
        protected abstract Task<TData> FindDataAsync(TAddDTO modelDTO);
        protected abstract Task<List<TData>> FindPageDataAsync(int startItem, int countItem);

        protected abstract string EntityAlreadyExist { get; }
        protected abstract string EntityNotFound { get; }
        protected abstract string EntitiesNotFound { get; }

        protected IUnitOfWork<LaborProtectionContext> UnitOfWork { get; private set; }
        public IStringLocalizer<SharedResource> Localizer { get; set; }

        public AbstractCRUDValidatorDTO(IUnitOfWork<LaborProtectionContext> unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<IAppActionResult> ValidateAdd(TAddDTO model)
        {
            IAppActionResult<TData> result = new AppActionResult<TData>();
            result.Data = await FindDataAsync(model);
            if (result.Data != null)
                result.ErrorMessages.Add(Localizer[EntityAlreadyExist]);
            return result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
        }

        public virtual async Task<IAppActionResult<TData>> ValidateGetData(Guid id)
        {
            IAppActionResult<TData> result = new AppActionResult<TData>();
            result.Data = await FindDataAsync(id);
            if (result.Data == null)
                result.ErrorMessages.Add(Localizer[EntityNotFound]);
            result.SetStatus(HttpStatusCode.NotFound, HttpStatusCode.OK);
            return result;
        }

        public virtual async Task<IAppActionResult<List<TData>>> ValidateGetData(int startItem, int countItem)
        {
            IAppActionResult<List<TData>> result = new AppActionResult<List<TData>>();
            result.Data = await FindPageDataAsync(startItem, countItem);
            if (result.Data == null)
                result.ErrorMessages.Add(Localizer[EntitiesNotFound]);
            result.SetStatus(HttpStatusCode.NotFound, HttpStatusCode.OK);
            return result;
        }

        public virtual async Task<IAppActionResult<TData>> ValidateUpdate(TUpdateDTO model)
        {
            return await ValidateFind(new AppActionResult<TData>(), model.Id);
        }

        public virtual async Task<IAppActionResult<TData>> ValidateDelete(Guid id)
        {
            return await ValidateFind(new AppActionResult<TData>(), id);
        }

        public virtual async Task<IAppActionResult<int>> ValidateCount()
        {
            IAppActionResult<int> result = new AppActionResult<int>();
            result.Data = await GetCountElementAsync();
            if (result.Data == 0)
                result.ErrorMessages.Add(Localizer[EntitiesNotFound]);
            result.SetStatus(HttpStatusCode.NotFound, HttpStatusCode.OK);
            return result;
        }

        private async Task<IAppActionResult<TData>> ValidateFind(IAppActionResult<TData> result, Guid id)
        {
            result.Data = await FindDataAsync(id);
            if (result.Data == null)
                result.ErrorMessages.Add(Localizer[EntityNotFound]);
            result.SetStatus(HttpStatusCode.NotFound, HttpStatusCode.OK);
            return result;
        }
    }
}
