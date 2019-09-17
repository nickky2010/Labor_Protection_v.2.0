using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IAppActionResult<TDTO>: IAppActionResult
    {
        TDTO Data { get; set; }
    }
    public interface IAppActionResult
    {
        int Status { get; set; }
        bool IsSuccess { get; }
        IList<string> ErrorMessages { get; }
    }
}
