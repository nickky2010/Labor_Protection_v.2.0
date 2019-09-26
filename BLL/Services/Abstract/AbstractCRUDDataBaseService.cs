using AutoMapper;
using BLL.Infrastructure;
using BLL.Infrastructure.Extentions;
using BLL.Interfaces;
using DAL.Interfaces;
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
            base(unitOfWorkService, mapper)  { }

        protected IValidatorDTO<TAddDTO, TUpdateDTO, TData> Validator { get; set; }

        protected abstract void AddDataToDbAsync(TData data);
        protected abstract void DeleteDataFromDbAsync(TData data);
        protected abstract void UpdateDataInDbAsync(TData data);
        protected abstract Task<TData> FindDataAsync(Guid id);

        public virtual async Task<IAppActionResult<TGetDTO>> AddAsync(TAddDTO modelDTO)
        {
            var result = new AppActionResult<TGetDTO>();
            result.SetResult(await Validator.ValidateAdd(modelDTO));
            if (!result.IsSuccess)
                return result;
            var data = Mapper.Map<TAddDTO, TData>(modelDTO);
            AddDataToDbAsync(data);
            await UnitOfWork.SaveChangesAsync();
            data = await FindDataAsync(data.Id);
            result.Data = Mapper.Map<TData, TGetDTO>(data);
            return result;
        }
        
        public virtual async Task<IAppActionResult> DeleteAsync(Guid id)
        {
            var result = new AppActionResult();
            var resultData = await Validator.ValidateDelete(id);
            result.SetResult(resultData);
            if (!result.IsSuccess)
                return result;
            DeleteDataFromDbAsync(resultData.Data);
            await UnitOfWork.SaveChangesAsync();
            return result;
        }

        public virtual async Task<IAppActionResult<TGetDTO>> GetAsync(Guid id)
        {
            var resultDTO = new AppActionResult<TGetDTO>();
            var resultData = await Validator.ValidateGetData(id);
            resultDTO.SetResult(resultData);
            if (resultData.Data != null)
                resultDTO.Data = Mapper.Map<TData, TGetDTO>(resultData.Data);
            return resultDTO;
        }

        public virtual async Task<IAppActionResult<List<TGetDTO>>> GetPageAsync(int startItem, int countItem)
        {
            var resultDTO = new AppActionResult<List<TGetDTO>>();
            var resultData = await Validator.ValidateGetData(startItem, countItem);
            resultDTO.SetResult(resultData);
            if (resultData.Data != null)
                resultDTO.Data = Mapper.Map<List<TData>, List<TGetDTO>>(resultData.Data);
            return resultDTO;
        }

        public virtual async Task<IAppActionResult<TGetDTO>> UpdateAsync(TUpdateDTO modelDTO)
        {
            var resultDTO = new AppActionResult<TGetDTO>();
            var resultData = await Validator.ValidateUpdate(modelDTO);
            resultDTO.SetResult(resultData);
            if (!resultDTO.IsSuccess)
                return resultDTO;
            Mapper.Map(modelDTO, resultData.Data);
            UpdateDataInDbAsync(resultData.Data);
            await UnitOfWork.SaveChangesAsync();
            resultDTO.Data = Mapper.Map<TData, TGetDTO>(resultData.Data);
            return resultDTO;
        }

        public virtual async Task<IAppActionResult<int>> GetCountElementAsync()
        {
            return await Validator.ValidateCount();
        }
    }
}
