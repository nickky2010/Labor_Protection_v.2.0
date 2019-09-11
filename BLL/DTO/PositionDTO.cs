using BLL.Interfaces;
using DAL.Extentions;
using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class PositionDTO : IDataDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ISet<EmployeeDTO> Employees { get; set; }
        public PositionDTO()
        {
            Employees = new HashSet<EmployeeDTO>();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is PositionDTO)) return false;
            PositionDTO p = (PositionDTO)obj;
            return Id.Equals(p.Id);
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^ 31 + Name.ToInt();
        }
    }
}
