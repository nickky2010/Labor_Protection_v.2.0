using DAL.EFContexts.Contexts;
using DAL.Interfaces;

namespace BLL.Interfaces
{
    public interface IUnitOfWorkService
    {
        IUnitOfWork<LaborProtectionContext> UnitOfWorkLaborProtectionContext { get; }
    }
}
