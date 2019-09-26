using Microsoft.Extensions.Localization;

namespace BLL.Interfaces
{
    public interface IService
    {
        IStringLocalizer<SharedResource> Localizer { get; set; }
    }
}
