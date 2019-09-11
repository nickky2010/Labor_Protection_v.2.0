using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IAppActionResult
    {
        int Status { get; }
        bool IsSuccess { get; }
        IList<string> ErrorMessages { get; }
    }
    public interface IAppActionResult<TDTO> : IAppActionResult
    {
        TDTO Data { get; set; }
    }
}
