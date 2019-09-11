using AutoMapper;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.Services;

namespace BLL.Interfaces
{
    public interface IService<Context>
        where Context : LaborProtectionContext
    {
        IStringLocalizer<SharedResource> Localizer { get; set; }
        IUnitOfWork<Context> UnitOfWork { get; }
        IMapper Mapper { get; }
    }
}
