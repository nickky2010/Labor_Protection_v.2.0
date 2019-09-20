using BLL.Interfaces;
using BLL.ValidatorsOfDTO.Abstract;
using ClosedXML.Excel;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorExcelFile : AbstractValidatorOfUploadFile<XLWorkbook>
    {
        public ValidatorExcelFile(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }

        public override string ErrorMessage => "FileNotXLWorkbook";

        public override IAppActionResult<XLWorkbook> ValidateFile(IFormFile file)
        {
            try
            {
                using (ResultFileType.Data = new XLWorkbook(file.OpenReadStream()))
                {
                }
            }
            catch
            {
                ResultFileType.ErrorMessages.Add(Localizer[ErrorMessage]);
            }
            SetStatus(ResultFileType, System.Net.HttpStatusCode.BadRequest, System.Net.HttpStatusCode.OK);
            return ResultFileType;
        }
    }
}
