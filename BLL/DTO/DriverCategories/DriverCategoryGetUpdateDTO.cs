using BLL.Interfaces;
using DAL.Extentions;
using System;

namespace BLL.DTO.DriverCategories
{
    public class DriverCategoryGetUpdateDTO : AbstractDataDTO<DriverCategoryGetUpdateDTO>, IUpdateDTO, IGetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^= 31 + Name.ToInt();
        }
    }
}