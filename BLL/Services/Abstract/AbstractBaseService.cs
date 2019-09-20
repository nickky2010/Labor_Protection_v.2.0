using AutoMapper;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;

namespace BLL.Services.Abstract
{
    internal abstract class AbstractBaseService : 
        IService<LaborProtectionContext>
    {
        public IUnitOfWork<LaborProtectionContext> UnitOfWork { get; protected set; }
        public IMapper Mapper { get; protected set; }
        public IStringLocalizer<SharedResource> Localizer { get; set; }

        public AbstractBaseService(IUnitOfWorkService unitOfWorkService, IMapper mapper)
        {
            UnitOfWork = unitOfWorkService.UnitOfWorkLaborProtectionContext;
            Mapper = mapper;
        }
    }
}
