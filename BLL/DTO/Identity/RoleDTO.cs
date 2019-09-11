using BLL.Interfaces;
using DAL.Extentions;
using System.Collections.Generic;

namespace BLL.DTO.Identity
{
    public class RoleDTO : IDataDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ISet<UserDTO> Users { get; set; }

        public RoleDTO()
        {
            Users = new HashSet<UserDTO>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is RoleDTO)) return false;
            RoleDTO r = (RoleDTO)obj;
            return Id==r.Id;
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToInt();
            return hash ^ 31 + Name.ToInt();
        }
    }
}
