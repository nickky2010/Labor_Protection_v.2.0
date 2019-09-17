using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    internal interface IValidatorService<TGetDTO, TAddDTO, TUpdateDTO, TData>
        where TGetDTO : IGetDTO
        where TAddDTO : IAddDTO
        where TUpdateDTO : IUpdateDTO
        where TData : IData
    {
        IUnitOfWork<LaborProtectionContext> UnitOfWork { get; set; }
        IStringLocalizer<SharedResource> Localizer { get; set; }
        IAppActionResult<TGetDTO> GetResult { get; set; }
        IAppActionResult<List<TGetDTO>> GetListResult { get; set; }
        IAppActionResult<TUpdateDTO> UpdateResult { get; set; }
        IAppActionResult DeleteResult { get; set; }

        string EntityAlreadyExist { get; }
        string EntityNotFound { get; }
        string EntitiesNotFound { get; }

        Task<IAppActionResult<TGetDTO>> ValidateAdd(TData data, TAddDTO model, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess);
        Task<IAppActionResult<TUpdateDTO>> ValidateUpdate(TData data, TUpdateDTO model, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess);
        IAppActionResult<TUpdateDTO> ValidateDataFromDbForUpdate(TData data, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess);
        IAppActionResult<TGetDTO> ValidateDataFromDb(TData data, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess);
        IAppActionResult<List<TGetDTO>> ValidateDataFromDb(IList<TData> data, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess);
        IAppActionResult ValidateDeleteDataFromDb(TData data, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess);
    }
}