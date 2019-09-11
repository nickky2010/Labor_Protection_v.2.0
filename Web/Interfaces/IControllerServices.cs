﻿using AutoMapper;
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL.EFContexts.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Web.Interfaces
{
    public interface IControllerServices<Source,S>
        where Source : Controller
        where S : IService<LaborProtectionContext>
    {
        IStringLocalizer<SharedResource> Localizer { get; }
        IMapper Mapper { get; }
        S Service { get; }
    }
}