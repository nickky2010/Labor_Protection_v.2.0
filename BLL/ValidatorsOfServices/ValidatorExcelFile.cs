using BLL.Interfaces;
using BLL.ValidatorsOfServices.Abstract;
using ClosedXML.Excel;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfServices
{
    internal class ValidatorExcelFile : AbstractValidatorOfUploadDataFromFileServices<XLWorkbook>
    {
        public ValidatorExcelFile(IUnitOfWork<LaborProtectionContext> unitOfWork) 
            : base(unitOfWork) { }

        public override string ErrorMessage => "FileNotXLWorkbook";

        public override IAppActionResult<XLWorkbook> ValidateFile(IFormFile file, IStringLocalizer<SharedResource> localizer)
        {
            try
            {
                using (ResultFileType.Data = new XLWorkbook(file.OpenReadStream()))
                {
                }
            }
            catch
            {
                ResultFileType.ErrorMessages.Add(localizer[ErrorMessage]);
            }
            SetStatus(ResultFileType, System.Net.HttpStatusCode.BadRequest, System.Net.HttpStatusCode.OK);
            return ResultFileType;
        }
    }
}
