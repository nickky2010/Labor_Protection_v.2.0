using AutoMapper;
using DAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BLL.Interfaces
{
    public interface IService<Context>
        where Context : DbContext
    {
        IStringLocalizer<SharedResource> Localizer { get; set; }
        IUnitOfWork<Context> UnitOfWork { get; }
        IMapper Mapper { get; }
        IHostingEnvironment Environment { get; set; }
    }
}
