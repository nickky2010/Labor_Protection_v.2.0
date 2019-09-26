using BLL.Infrastructure;
using BLL.Interfaces;
using BLL;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Net;
using Web.Interfaces;
using BLL.Infrastructure.Extentions;

namespace Web.ValidatorsOfControllers.Abstract
{
    internal abstract class AbstractValidatorOfControllers<TGetDTO, TAddDTO, TUpdateDTO> :
        AbstractBaseValidatorOfControllers,
        IValidatorCRUDController<TGetDTO, TAddDTO, TUpdateDTO>
        where TGetDTO : IGetDTO
        where TUpdateDTO : IUpdateDTO
        where TAddDTO : IAddDTO
    {
        public virtual string StartItemNotExist { get => "StartItemNotExist"; }
        public virtual string CountItemsLeastOne { get => "CountItemsLeastOne"; }
        public virtual string DataIsNotValid { get => "DataIsNotValid"; }
        
        public AbstractValidatorOfControllers(IStringLocalizer<SharedResource> localizer)
            : base(localizer) { }

        public virtual IAppActionResult<List<TGetDTO>> ValidatePaging(int startItem, int countItem)
        {
            var result = new AppActionResult<List<TGetDTO>>();
            if (startItem < 0)
                result.ErrorMessages.Add(Localizer[StartItemNotExist]);
            if (countItem < 1)
                result.ErrorMessages.Add(Localizer[CountItemsLeastOne]);
            result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return result;
        }

        public virtual IAppActionResult<TGetDTO> ValidateAdd(TAddDTO addDTO, ModelStateDictionary modelState)
        {
            var result = new AppActionResult<TGetDTO>();
            if (addDTO == null)
                result.ErrorMessages.Add(Localizer[NoData]);
            if (!modelState.IsValid)
                result.ErrorMessages.Add(Localizer[DataIsNotValid]);
            result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return result;
        }

        public virtual IAppActionResult<TGetDTO> ValidateUpdate(TUpdateDTO updateDTO, ModelStateDictionary modelState)
        {
            var result = new AppActionResult<TGetDTO>();
            if (updateDTO == null)
                result.ErrorMessages.Add(Localizer[NoData]);
            if (!modelState.IsValid)
                result.ErrorMessages.Add(Localizer[DataIsNotValid]);
            result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return result;
        }
    }
}
