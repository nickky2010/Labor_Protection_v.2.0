using AutoMapper;
using BLL.Interfaces;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.ValidatorsOfControllers;
using Web.Controllers.Abstract;
using ClosedXML.Excel;
using BLL.Infrastructure.Readers.ReadModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class UploadDataFromExcelController : AbstractFileController<XLWorkbook, ReadModelForExcel>
    {
        public UploadDataFromExcelController(IStringLocalizer<SharedResource> localizer, IMapper mapper, 
            IUploadDataFromFileService<XLWorkbook, ReadModelForExcel> service) : base(localizer, mapper, service)
        {
            Validator = new ValidatorExcelFileController(Localizer);
        }
    }
}
