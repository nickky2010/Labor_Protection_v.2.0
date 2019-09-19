using AutoMapper;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;

namespace BLL.Services.Abstract
{
    internal abstract class AbstractBaseService 
    {
        public IStringLocalizer<SharedResource> Localizer { get; set; }
        public IUnitOfWork<LaborProtectionContext> UnitOfWork { get; protected set; }
        public IMapper Mapper { get; protected set; }

        public AbstractBaseService(IUnitOfWorkService unitOfWorkService, IMapper mapper)
        {
            UnitOfWork = unitOfWorkService.UnitOfWorkLaborProtectionContext;
            Mapper = mapper;
        }
    }
}
