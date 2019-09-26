using AutoMapper;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;

namespace BLL.Services.Abstract
{
    internal abstract class AbstractBaseService : IService
    {
        protected IUnitOfWork<LaborProtectionContext> UnitOfWork { get; private set; }
        protected IMapper Mapper { get; private set; }
        public IStringLocalizer<SharedResource> Localizer { get; set; }

        public AbstractBaseService(IUnitOfWorkService unitOfWorkService, IMapper mapper)
        {
            UnitOfWork = unitOfWorkService.UnitOfWorkLaborProtectionContext;
            Mapper = mapper;
        }
    }
}
