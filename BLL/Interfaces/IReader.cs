using Microsoft.Extensions.Localization;

namespace BLL.Interfaces
{
    internal interface IReader<FileType, ReadModel>
        where FileType : class
        where ReadModel : IReadModel
    {
        int DataModelColCount { get; set; }
        IStringLocalizer<SharedResource> Localizer { get; set; }
        IAppActionResult<ReadModel> Result { get; set; }


        IAppActionResult<ReadModel> Read(FileType file);
    }
}
