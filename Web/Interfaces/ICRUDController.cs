using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Interfaces
{
    public interface ICRUDController<TAddViewModel, TUpdateViewModel>
        where TAddViewModel : IAddViewModel
        where TUpdateViewModel : IUpdateViewModel
    {
        Task<IAppActionResult> Get([FromQuery]int startItem, [FromQuery]int countItem);
        Task<IAppActionResult> Get(Guid guid);
        Task<IAppActionResult> Post([FromBody]TAddViewModel viewModel);
        Task<IAppActionResult> Put([FromBody]TUpdateViewModel viewModel);
        Task<IAppActionResult> Delete(Guid guid);
    }
}
