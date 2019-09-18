using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces
{
    public interface IAddUpdatePhotoDTO
    {
        IFormFile Picture { get; set; }
    }
    public interface IGetPhotoDTO
    {
        byte[] Picture { get; set; }
    }
}