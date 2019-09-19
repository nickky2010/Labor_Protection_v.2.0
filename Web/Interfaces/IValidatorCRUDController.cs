using BLL.Interfaces;
using BLL;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Web.Interfaces
{
    public interface IValidatorCRUDController<TGetDTO, TAddDTO, TUpdateDTO>
        where TGetDTO : IGetDTO
        where TUpdateDTO : IUpdateDTO
        where TAddDTO : IAddDTO
    {
        IStringLocalizer<SharedResource> Localizer { get; set; }
        IAppActionResult<TGetDTO> GetResult { get; set; }
        IAppActionResult<List<TGetDTO>> GetListResult { get; set; }
        IAppActionResult Result { get; set; }
        string StartItemNotExist { get; }
        string CountItemsLeastOne { get; }
        string NoData { get; }
        string DataIsNotValid { get; }

        IAppActionResult<List<TGetDTO>> ValidatePaging(int startItem, int countItem);
        IAppActionResult<TGetDTO> ValidateAdd(TAddDTO addDTO, ModelStateDictionary modelState);
        IAppActionResult<TGetDTO> ValidateUpdate(TUpdateDTO updateDTO, ModelStateDictionary modelState);
    }
}
