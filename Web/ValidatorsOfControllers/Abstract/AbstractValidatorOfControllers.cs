using BLL.Infrastructure;
using BLL.Interfaces;
using BLL;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Net;
using Web.Interfaces;

namespace Web.ValidatorsOfControllers.Abstract
{
    internal abstract class AbstractValidatorOfControllers<TGetDTO, TAddDTO, TUpdateDTO> :
        AbstractBaseValidatorOfControllers,
        IValidatorCRUDController<TGetDTO, TAddDTO, TUpdateDTO>

        where TGetDTO : IGetDTO
        where TUpdateDTO : IUpdateDTO
        where TAddDTO : IAddDTO
    {
        public IAppActionResult<TGetDTO> GetResult { get; set; }
        public IAppActionResult<List<TGetDTO>> GetListResult { get; set; }

        public virtual string StartItemNotExist { get => "StartItemNotExist"; }
        public virtual string CountItemsLeastOne { get => "CountItemsLeastOne"; }
        public virtual string DataIsNotValid { get => "DataIsNotValid"; }
        
        public AbstractValidatorOfControllers(IStringLocalizer<SharedResource> localizer): base(localizer)
        {
            GetResult = new AppActionResult<TGetDTO>();
            GetListResult = new AppActionResult<List<TGetDTO>>();
        }

        public virtual IAppActionResult<List<TGetDTO>> ValidatePaging(int startItem, int countItem)
        {
            if (startItem < 1)
                GetListResult.ErrorMessages.Add(Localizer[StartItemNotExist]);
            if (countItem < 1)
                GetListResult.ErrorMessages.Add(Localizer[CountItemsLeastOne]);
            SetStatus(GetListResult, HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return GetListResult;
        }

        public virtual IAppActionResult<TGetDTO> ValidateAdd(TAddDTO addDTO, ModelStateDictionary modelState)
        {
            if (addDTO == null)
                GetResult.ErrorMessages.Add(Localizer[NoData]);
            if (!modelState.IsValid)
                GetResult.ErrorMessages.Add(Localizer[DataIsNotValid]);
            SetStatus(GetResult, HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return GetResult;
        }

        public virtual IAppActionResult<TGetDTO> ValidateUpdate(TUpdateDTO updateDTO, ModelStateDictionary modelState)
        {
            if (updateDTO == null)
                GetResult.ErrorMessages.Add(Localizer[NoData]);
            if (!modelState.IsValid)
                GetResult.ErrorMessages.Add(Localizer[DataIsNotValid]);
            SetStatus(GetResult, HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return GetResult;
        }
    }
}
