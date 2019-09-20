using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    internal abstract class AbstractCRUDDataBaseService<TGetDTO, TAddDTO, TUpdateDTO, TData> :  AbstractBaseService,
        ICRUDDataBaseService<TGetDTO, TAddDTO, TUpdateDTO>
        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        public AbstractCRUDDataBaseService(IUnitOfWorkService unitOfWorkService, IMapper mapper) : 
            base(unitOfWorkService, mapper)
        {
            DataResult = new AppActionResult<TGetDTO>();
            DataListResult = new AppActionResult<List<TGetDTO>>();
        }

        protected IValidatorDTO<TGetDTO, TAddDTO, TUpdateDTO, TData> Validator { get; set; }
        public IAppActionResult<TGetDTO> DataResult { get; set; }
        public IAppActionResult<List<TGetDTO>> DataListResult { get; set; }

        protected abstract void AddDataToDbAsync(TData data);
        protected abstract void DeleteDataFromDbAsync(TData data);
        protected abstract void UpdateDataInDbAsync(TData data);
        protected abstract Task<TData> FindDataAsync(Guid id);
        public virtual async Task<IAppActionResult<TGetDTO>> AddAsync(TAddDTO modelDTO)
        {
            SetEmptyDataResult(await Validator.ValidateAdd(modelDTO));
            if (!DataResult.IsSuccess)
                return DataResult;
            var data = Mapper.Map<TAddDTO, TData>(modelDTO);
            AddDataToDbAsync(data);
            await UnitOfWork.SaveChangesAsync();
            data = await FindDataAsync(data.Id);
            DataResult.Data = Mapper.Map<TData, TGetDTO>(data);
            return DataResult;
        }
        
        public virtual async Task<IAppActionResult> DeleteAsync(Guid id)
        {
            var result = await Validator.ValidateDelete(id);
            if (!result.IsSuccess)
                return result;
            DeleteDataFromDbAsync(result.Data);
            await UnitOfWork.SaveChangesAsync();
            return result;
        }

        public virtual async Task<IAppActionResult<TGetDTO>> GetAsync(Guid id)
        {
            return SetDataResult(await Validator.ValidateGetData(id));
        }

        public virtual async Task<IAppActionResult<List<TGetDTO>>> GetPageAsync(int startItem, int countItem)
        {
            return SetDataResult(await Validator.ValidateGetData(startItem, countItem));
        }

        public virtual async Task<IAppActionResult<TGetDTO>> UpdateAsync(TUpdateDTO modelDTO)
        {
            var result = await Validator.ValidateUpdate(modelDTO);
            if (!result.IsSuccess)
                return SetEmptyDataResult(result);
            Mapper.Map(modelDTO, result.Data);
            UpdateDataInDbAsync(result.Data);
            await UnitOfWork.SaveChangesAsync();
            return SetDataResult(result);
        }

        protected IAppActionResult<TGetDTO> SetEmptyDataResult(IAppActionResult result)
        {
            SetBaseResult(result, DataResult);
            return DataResult;
        }
        protected IAppActionResult<TGetDTO> SetDataResult(IAppActionResult<TData> result)
        {
            if (result.Data != null)
                DataResult.Data = Mapper.Map<TData, TGetDTO>(result.Data);
            SetBaseResult(result, DataResult);
            return DataResult;
        }

        protected IAppActionResult<List<TGetDTO>> SetDataResult(IAppActionResult<List<TData>> result)
        {
            if (result.Data != null)
                DataListResult.Data = Mapper.Map<List<TData>, List<TGetDTO>>(result.Data);
            SetBaseResult(result, DataListResult);
            return DataListResult;
        }
        protected void SetBaseResult(IAppActionResult sourceResult, IAppActionResult destResult)
        {
            destResult.ErrorMessages = sourceResult.ErrorMessages;
            destResult.Status = sourceResult.Status;
        }
    }
}
