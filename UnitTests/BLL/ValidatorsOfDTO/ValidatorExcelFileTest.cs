using BLL;
using BLL.Interfaces;
using BLL.ValidatorsOfDTO;
using ClosedXML.Excel;
using Microsoft.Extensions.Localization;
using UnitTests.BLL.ValidatorsOfDTO.AbstractValidatorDTOTest;

namespace UnitTests.BLL.ValidatorsOfDTO
{
    public class ValidatorExcelFileTest :
        AbstractFileValidatorDTOTest<XLWorkbook>
    {
        protected override string CorrectFile => "db.xlsx";
        protected override string IncorrectFile => "testPhoto_100x100.jpeg";

        protected override IValidatorOfUploadFile<XLWorkbook> CreateValidator(IStringLocalizer<SharedResource> localizer)
        {
            return new ValidatorExcelFile(localizer);
        }
    }
}
