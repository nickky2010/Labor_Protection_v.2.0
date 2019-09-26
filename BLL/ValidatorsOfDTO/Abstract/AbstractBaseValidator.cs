using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfDTO.Abstract
{
    internal abstract class AbstractBaseValidator
    {
        protected IUnitOfWork<LaborProtectionContext> UnitOfWork { get; private set; }
        protected IStringLocalizer<SharedResource> Localizer { get; private set; }

        public AbstractBaseValidator(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            UnitOfWork = unitOfWork;
            Localizer = localizer;
        }
    }
}
