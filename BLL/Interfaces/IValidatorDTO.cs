using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IValidatorDTO<TAddDTO, TUpdateDTO, TData> : ILocalService
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        Task<IAppActionResult<TData>> ValidateGetData(Guid id);
        Task<IAppActionResult<List<TData>>> ValidateGetData(int startItem, int countItem);
        Task<IAppActionResult> ValidateAdd(TAddDTO model);
        Task<IAppActionResult<TData>> ValidateUpdate(TUpdateDTO model);
        Task<IAppActionResult<TData>> ValidateDelete(Guid id);
        Task<IAppActionResult<int>> ValidateCount();
    }

    public interface IValidatorOfUploadFile<FileType> : ILocalService
        where FileType : class
    {
        IAppActionResult<FileType> ValidateFile(IFormFile file);
    }
}