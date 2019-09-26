using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Web.Interfaces
{
    public interface IValidatorCRUDController<TGetDTO, TAddDTO, TUpdateDTO>
        where TGetDTO : IGetDTO
        where TUpdateDTO : IUpdateDTO
        where TAddDTO : IAddDTO
    {
        IAppActionResult<List<TGetDTO>> ValidatePaging(int startItem, int countItem);
        IAppActionResult<TGetDTO> ValidateAdd(TAddDTO addDTO, ModelStateDictionary modelState);
        IAppActionResult<TGetDTO> ValidateUpdate(TUpdateDTO updateDTO, ModelStateDictionary modelState);
    }
}
