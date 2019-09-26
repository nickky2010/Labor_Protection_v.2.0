namespace BLL.Interfaces
{
    internal interface IReader<FileType, ReadModel>
        where FileType : class
        where ReadModel : IReadModel
    {
        IAppActionResult<ReadModel> Read(FileType file);
    }
}
