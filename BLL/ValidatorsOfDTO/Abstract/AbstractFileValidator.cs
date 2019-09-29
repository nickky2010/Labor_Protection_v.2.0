using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfDTO.Abstract
{
    internal abstract class AbstractFileValidator
    {
        protected IStringLocalizer<SharedResource> Localizer { get; private set; }

        public AbstractFileValidator(IStringLocalizer<SharedResource> localizer)
        {
            Localizer = localizer;
        }
    }
}
