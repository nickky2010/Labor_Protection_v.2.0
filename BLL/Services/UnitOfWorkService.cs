using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Repositories.EF;

namespace BLL.Services
{
    internal class UnitOfWorkService : IUnitOfWorkService
    {
        public string ConnectionString { get; private set; }
        public UnitOfWorkService(string connectionString)
        {
            ConnectionString = connectionString;
            
        }
        public IUnitOfWork<LaborProtectionContext> UnitOfWorkLaborProtectionContext =>
            new EFUnitOfWork<LaborProtectionContext>(new LaborProtectionContext(ConnectionString));
    }
}
