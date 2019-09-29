using BLL;
using BLL.Interfaces;
using BLL.ValidatorsOfDTO.Abstract;
using Microsoft.Extensions.Localization;
using System.Drawing;
using UnitTests.BLL.ValidatorsOfDTO.AbstractValidatorDTOTest;

namespace UnitTests.BLL.ValidatorsOfDTO
{
    public class ValidatorPhotoFileTest :
        AbstractFileValidatorDTOTest<Image>
    {
        protected override string CorrectFile => "testPhoto_100x100.jpeg";
        protected override string IncorrectFile => "db.xlsx";

        protected override IValidatorOfUploadFile<Image> CreateValidator(IStringLocalizer<SharedResource> localizer)
        {
            return new ValidatorPhotoFile(localizer);
        }
    }
}
