using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;

namespace BLL.Infrastructure.Dependency
{
    public class InterfacesRegistrationsBLL : NinjectModule
    {
        public string ConnectionString { get; private set; }
        public InterfacesRegistrationsBLL(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IUnitOfWorkService>().To<UnitOfWorkService>().WithConstructorArgument(ConnectionString);
        }
    }
}
