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
    internal interface IValidatorCRUDDataBaseService<TGetDTO, TAddDTO, TUpdateDTO, TData>:
        IValidatorDataBaseService<TGetDTO>
        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        Task<IAppActionResult<TGetDTO>> ValidateUpdate(TData data, TUpdateDTO model, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer);
        IAppActionResult<TGetDTO> ValidateDataFromDb(TData data, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer);
        Task<IAppActionResult<TGetDTO>> ValidateAdd(TData data, TAddDTO model, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer);
        IAppActionResult<List<TGetDTO>> ValidateDataFromDb(IList<TData> data, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer);
        IAppActionResult ValidateDeleteDataFromDb(TData data, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer);
    }

    internal interface IValidatorCRUDDataBasePhotoService<TGetDTO, TAddDTO, TUpdateDTO, TData> :
    IValidatorDataBaseService<TGetDTO>
        where TGetDTO : IGetDTO, IGetPhotoDTO
        where TAddDTO : IAddDTO, IAddUpdatePhotoDTO
        where TUpdateDTO : IUpdateDTO, IAddUpdatePhotoDTO
        where TData : IData
    {
        Task<IAppActionResult<TGetDTO>> ValidateUpdate(string contentPath, TData data, TUpdateDTO model, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer);
        IAppActionResult<TGetDTO> ValidateDataFromDbForUpdate(string contentPath, TData data, Guid id,
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer);
        Task<IAppActionResult<TGetDTO>> ValidateAdd(string contentPath, TData data, TAddDTO model, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer);
        IAppActionResult<TGetDTO> ValidateDataFromDb(string contentPath, TData data, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer);
        IAppActionResult<List<TGetDTO>> ValidateDataFromDb(string contentPath, IList<TData> data, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer);
        IAppActionResult ValidateDeleteDataFromDb(string contentPath, TData data, 
            HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess, IStringLocalizer<SharedResource> localizer);
    }

    internal interface IValidatorDataBaseService<TGetDTO>:
        IBaseValidatorService
        where TGetDTO : IGetDTO
    {
        IAppActionResult<TGetDTO> GetResult { get; set; }
        IAppActionResult<List<TGetDTO>> GetListResult { get; set; }
    }
    
    internal interface IBaseValidatorService
    {
        IUnitOfWork<LaborProtectionContext> UnitOfWork { get; set; }
        IAppActionResult Result { get; set; }
    }

    internal interface IValidatorUploadDataFromFileService<FileType> :
        IBaseValidatorService
        where FileType: class
    {
        IAppActionResult<FileType> ValidateFile(IFormFile file, IStringLocalizer<SharedResource> localizer);
        void SetStatus(IAppActionResult appActionResult, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess);
    }

    internal interface IValidatorUploadDataFromFileForCRUDService<FileType> :
    IBaseValidatorService
        where FileType : class
    {
        IAppActionResult<FileType> ValidateFile(IFormFile file, IAppActionResult result, IStringLocalizer<SharedResource> localizer);
    }
}