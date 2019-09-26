using BLL.Infrastructure;
using BLL.Infrastructure.Extentions;
using BLL.Interfaces;
using BLL.ValidatorsOfDTO.Abstract;
using ClosedXML.Excel;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Net;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorExcelFile : AbstractBaseValidator, 
        IValidatorOfUploadFile<XLWorkbook>
    {
        public ValidatorExcelFile(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }

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
            }
            result.SetStatus(HttpStatusCode.BadRequest, HttpStatusCode.OK);
            return result;
        }
    }
}
