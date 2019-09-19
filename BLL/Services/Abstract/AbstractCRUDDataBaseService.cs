﻿using AutoMapper;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
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
            base(unitOfWorkService, mapper) { }

        protected IValidatorCRUDDataBaseService<TGetDTO, TAddDTO, TUpdateDTO, TData> Validator { get; set; }

        protected abstract void AddDataToDbAsync(TData data);
        protected abstract void DeleteDataFromDbAsync(TData data);
        protected abstract void UpdateDataInDbAsync(TData data);
        protected abstract Task<TData> FindDataAsync(Guid id);
        protected abstract Task<List<TData>> FindPageDataAsync(int startItem, int countItem);
        protected abstract Task<TData> FindDataIfAddAsync(TAddDTO modelDTO);
        protected abstract Task<TData> FindDataIfUpdateAsync(TUpdateDTO modelDTO);

        public virtual async Task<IAppActionResult<TGetDTO>> AddAsync(TAddDTO modelDTO)
        {
            var data = await FindDataIfAddAsync(modelDTO);
            var result = await Validator.ValidateAdd(data, modelDTO, HttpStatusCode.BadRequest, HttpStatusCode.OK, Localizer);
            if (!result.IsSuccess)
                return result;
            data = Mapper.Map<TAddDTO, TData>(modelDTO);
            AddDataToDbAsync(data);
            await UnitOfWork.SaveChangesAsync();
            data = await FindDataAsync(data.Id);
            result = Validator.ValidateDataFromDb(data, HttpStatusCode.InternalServerError, HttpStatusCode.Created, Localizer);
            if (!result.IsSuccess)
                return result;
            result.Data = Mapper.Map<TData, TGetDTO>(data);
            return result;
        }
        
        public virtual async Task<IAppActionResult> DeleteAsync(Guid id)
        {
            var data = await FindDataAsync(id);
            var result = Validator.ValidateDeleteDataFromDb(data, HttpStatusCode.NotFound, HttpStatusCode.OK, Localizer);
            if (!result.IsSuccess)
                return result;
            DeleteDataFromDbAsync(data);
            await UnitOfWork.SaveChangesAsync();
            return result;
        }

        public virtual async Task<IAppActionResult<TGetDTO>> GetAsync(Guid id)
        {
            var data = await FindDataAsync(id);
            var result = Validator.ValidateDataFromDb(data, HttpStatusCode.NotFound, HttpStatusCode.OK, Localizer);
            if (!result.IsSuccess)
                return result;
            result.Data = Mapper.Map<TData, TGetDTO>(data);
            return result;
        }

        public virtual async Task<IAppActionResult<List<TGetDTO>>> GetPageAsync(int startItem, int countItem)
        {
            var data = await FindPageDataAsync(startItem, countItem);
            var result = Validator.ValidateDataFromDb(data, HttpStatusCode.NotFound, HttpStatusCode.OK, Localizer);
            if (!result.IsSuccess)
                return result;
            result.Data = Mapper.Map<List<TData>, List<TGetDTO>>(data);
            return result;
        }

        public virtual async Task<IAppActionResult<TGetDTO>> UpdateAsync(TUpdateDTO modelDTO)
        {
            var data = await FindDataIfUpdateAsync(modelDTO);
            var result = await Validator.ValidateUpdate(data, modelDTO, HttpStatusCode.BadRequest, HttpStatusCode.OK, Localizer);
            if (!result.IsSuccess)
                return result;
            Mapper.Map(modelDTO, data);
            UpdateDataInDbAsync(data);
            await UnitOfWork.SaveChangesAsync();
            data = await FindDataAsync(data.Id);
            result = Validator.ValidateDataFromDb(data, HttpStatusCode.InternalServerError, HttpStatusCode.OK, Localizer);
            if (!result.IsSuccess)
                return result;
            result.Data = Mapper.Map<TData, TGetDTO>(data);
            return result;
        }
    }
}