using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Infrastructure
{
    public class ControllerExt<DTO, VModel>: Controller
        where DTO : IDataDTO
        where VModel : IViewModel
    {
        protected IAppActionResult SetResult(IAppActionResult result)
        {
            ControllerContext.HttpContext.Response.StatusCode = result.Status;
            return result;
        }

        protected IAppActionResult SetDataResult(IAppActionResult result, IMapper mapper)
        {
            if (result.IsSuccess)
            {
                var viewData = mapper.Map<DTO, VModel>(((AppActionResult<DTO>)result).Data);
                return SetResult(new AppActionResult<VModel> { Status = result.Status, Data = viewData });
            }
            return SetResult(result);
        }

        protected IAppActionResult SetDataArrayResult(IAppActionResult result, IMapper mapper)
        {
            if (result.IsSuccess)
            {
                var viewData = mapper.Map<IList<DTO>, IList<VModel>>(((AppActionResult<IList<DTO>>)result).Data);
                return SetResult(new AppActionResult<IList<VModel>> { Status = result.Status, Data = viewData });
            }
            return SetResult(result);
        }

    }
}
