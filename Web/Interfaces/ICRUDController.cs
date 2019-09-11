using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Interfaces
{
    public interface ICRUDController<TViewModel>
        where TViewModel : IViewModel
    {
        Task<IAppActionResult> Get([FromQuery]int startItem, [FromQuery]int countItem);
        Task<IAppActionResult> Get(Guid guid);
        Task<IAppActionResult> Post([FromBody]TViewModel viewModel);
        Task<IAppActionResult> Put([FromBody]TViewModel viewModel);
        Task<IAppActionResult> Delete(Guid guid);
    }
}
