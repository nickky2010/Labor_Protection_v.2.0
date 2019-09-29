using System;

namespace DAL.Interfaces
{
    public interface IData
    {
        Guid Id { get; set; }
    }
    public interface IPhotoData
    {
        byte[] Photo { get; set; }
    }
}
