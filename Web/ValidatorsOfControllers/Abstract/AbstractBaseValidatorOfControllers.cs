using BLL;
using Microsoft.Extensions.Localization;

namespace Web.ValidatorsOfControllers.Abstract
{
    internal abstract class AbstractBaseValidatorOfControllers
    {
        protected IStringLocalizer<SharedResource> Localizer { get; private set; }
        protected virtual string NoData { get => "NoData"; }

        public AbstractBaseValidatorOfControllers(IStringLocalizer<SharedResource> localizer)
        {
            Localizer = localizer;
        }
    }
}
