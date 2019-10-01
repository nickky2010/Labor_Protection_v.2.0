using BLL.Infrastructure;
using BLL.Infrastructure.Extentions;
using BLL.Interfaces;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Net;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorExcelFile : IValidatorOfUploadFile<XLWorkbook>
    {
        public IStringLocalizer<SharedResource> Localizer { get; set; }

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
