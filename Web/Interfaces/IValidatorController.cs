using BLL;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Web.Interfaces
{
    public interface IValidatorController<TGetDTO, TAddDTO, TUpdateDTO>
        where TGetDTO : IGetDTO
        where TUpdateDTO : IUpdateDTO
        where TAddDTO : IAddDTO
    {
        IStringLocalizer<SharedResource> Localizer { get; set; }
        IAppActionResult<TGetDTO> GetResult { get; set; }
        IAppActionResult<List<TGetDTO>> GetListResult { get; set; }
        IAppActionResult<TUpdateDTO> UpdateResult { get; set; }
        IAppActionResult DeleteResult { get; set; }
        string StartItemNotExist { get; }
        string CountItemsLeastOne { get; }
        string NoData { get; }
        string DataIsNotValid { get; }

        IAppActionResult<List<TGetDTO>> ValidatePaging(int startItem, int countItem);
        IAppActionResult<TGetDTO> ValidateAdd(TAddDTO addDTO, ModelStateDictionary modelState);
        IAppActionResult<TUpdateDTO> ValidateUpdate(TUpdateDTO updateDTO, ModelStateDictionary modelState);
    }
}
