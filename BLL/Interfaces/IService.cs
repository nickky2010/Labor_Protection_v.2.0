using Microsoft.Extensions.Localization;

namespace BLL.Interfaces
{
    public interface ILocalService
    {
        IStringLocalizer<SharedResource> Localizer { get; set; }
    }
}
