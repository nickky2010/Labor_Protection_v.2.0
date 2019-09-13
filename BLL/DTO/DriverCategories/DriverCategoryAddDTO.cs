using BLL.Interfaces;
using DAL.Extentions;

namespace BLL.DTO.DriverCategories
{
    public class DriverCategoryAddDTO : AbstractDataDTO<DriverCategoryAddDTO>, IAddDTO
    {
        public string Name { get; set; }
        public override int GetHashCode()
        {
            int hash = 17;
            return hash ^= 31 + Name.ToInt();
        }
    }
}