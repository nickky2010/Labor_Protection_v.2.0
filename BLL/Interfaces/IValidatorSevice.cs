using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    internal interface IValidatorDTO<TGetDTO, TAddDTO, TUpdateDTO, TData>:
        IValidatorDataBaseService<TData>
        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        Task<IAppActionResult<TData>> ValidateGetData(Guid id);
        Task<IAppActionResult<List<TData>>> ValidateGetData(int startItem, int countItem);
        Task<IAppActionResult> ValidateAdd(TAddDTO model);
        Task<IAppActionResult<TData>> ValidateUpdate(TUpdateDTO model);
        Task<IAppActionResult<TData>> ValidateDelete(Guid id);
    }

    internal interface IValidatorDataBaseService<TData> :
        IBaseValidator
        where TData : IData
    {
        IAppActionResult<TData> DataResult { get; set; }
        IAppActionResult<List<TData>> DataListResult { get; set; }
    }
    
    internal interface IBaseValidator
    {
        IUnitOfWork<LaborProtectionContext> UnitOfWork { get; set; }
        IAppActionResult Result { get; set; }
        IStringLocalizer<SharedResource> Localizer { get; set; }
    }

    internal interface IValidatorOfUploadFile<FileType> :
        IBaseValidator
        where FileType: class
    {
        IAppActionResult<FileType> ValidateFile(IFormFile file);
        void SetStatus(IAppActionResult appActionResult, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess);
    }

    internal interface IValidatorUploadDataFromFileForCRUDService<FileType> :
    IBaseValidator
        where FileType : class
    {
        IAppActionResult<FileType> ValidateFile(IFormFile file, IAppActionResult result);
    }
}