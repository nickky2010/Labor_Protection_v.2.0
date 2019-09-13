using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDriverCategoryRepository
    {
        Task<bool> AllContainNameAsync(IList<IDriverCategory> driverCategory);
    }
    public interface IDriverCategory
    {
        Guid DriverCategoryId { get; set; }
    }
}
