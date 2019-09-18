using System;

namespace BLL.Interfaces
{
    public interface IAddDTO { }
    public interface IUpdateDTO { }
    public interface IGetDTO
    {
        Guid Id { get; set; }
    }
}
