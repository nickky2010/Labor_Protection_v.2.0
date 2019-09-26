using AutoMapper;
using BLL;
using BLL.Infrastructure.Readers.ReadModels;
using BLL.Interfaces;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Web.Controllers.Abstract;
using Web.ValidatorsOfControllers;

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
