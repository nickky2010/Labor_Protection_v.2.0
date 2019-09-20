using System;

namespace BLL.Interfaces
{
    public interface IAddDTO { }
    public interface IUpdateDTO
    {
        Guid Id { get; set; }
    }
    public interface IGetDTO
    {
        Guid Id { get; set; }
    }
}
