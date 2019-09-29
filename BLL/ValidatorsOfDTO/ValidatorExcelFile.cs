using BLL.Infrastructure;
using BLL.Infrastructure.Extentions;
using BLL.Interfaces;
using BLL.ValidatorsOfDTO.Abstract;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Net;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorExcelFile : AbstractFileValidator,
        IValidatorOfUploadFile<XLWorkbook>
    {
        public ValidatorExcelFile(IStringLocalizer<SharedResource> localizer)
            : base(localizer) { }

        public IAppActionResult<XLWorkbook> ValidateFile(IFormFile file)
        {
            IAppActionResult<XLWorkbook> result = new AppActionResult<XLWorkbook>();
            try
            {
                using (result.Data = new XLWorkbook(file.OpenReadStream()))
                {
                }
            }
            catch
            {
                result.ErrorMessages.Add(Localizer["FileNotXLWorkbook"]);
                result.Data = null;
            }
            result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return result;
        }
    }
}
