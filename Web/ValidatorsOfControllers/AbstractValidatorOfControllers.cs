using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Net;
using Web.Interfaces;

namespace BLL.ValidatorsOfServices
{
    internal abstract class AbstractValidatorOfControllers<TGetDTO, TAddDTO, TUpdateDTO> : 
        IValidatorController<TGetDTO, TAddDTO, TUpdateDTO>

        where TGetDTO : IGetDTO
        where TUpdateDTO : IUpdateDTO
        where TAddDTO : IAddDTO
    {
        public IStringLocalizer<SharedResource> Localizer { get; set; }
        public IAppActionResult<TGetDTO> GetResult { get; set; }
        public IAppActionResult<List<TGetDTO>> GetListResult { get; set; }
        public IAppActionResult DeleteResult { get; set; }

        public virtual string StartItemNotExist { get => "StartItemNotExist"; }
        public virtual string CountItemsLeastOne { get => "CountItemsLeastOne"; }
        public virtual string NoData { get => "NoData"; }
        public virtual string DataIsNotValid { get => "DataIsNotValid"; }


        public AbstractValidatorOfControllers(IStringLocalizer<SharedResource> localizer)
        {
            GetResult = new AppActionResult<TGetDTO>();
            DeleteResult = new AppActionResult();
            GetListResult = new AppActionResult<List<TGetDTO>>();
            Localizer = localizer;
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

        protected void SetStatus(IAppActionResult appActionResult, HttpStatusCode statusCodeIsError, HttpStatusCode statusCodeIsSuccess)
        {
            if (appActionResult.ErrorMessages.Count != 0)
                appActionResult.Status = (int)statusCodeIsError;
            else
                appActionResult.Status = (int)statusCodeIsSuccess;
        }
    }
}
