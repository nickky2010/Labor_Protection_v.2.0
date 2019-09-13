using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IAppActionResult<TDTO>
    {
        int Status { get; }
        bool IsSuccess { get; }
        IList<string> ErrorMessages { get; }
        TDTO Data { get; set; }
    }
    public interface IAppActionResult
    {
        int Status { get; }
        bool IsSuccess { get; }
        IList<string> ErrorMessages { get; }
    }
}
