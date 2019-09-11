using DAL.Extentions;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DAL.Models.Identity
{
    public class Role : IdentityRole
    {
        public virtual ISet<User> Users { get; set; }
        public byte[] RowVersion { get; set; }

        public Role()
        {
            Users = new HashSet<User>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Role)) return false;
            Role r = (Role)obj;
            return Id == r.Id;
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToInt();
            return hash ^ 31 + Name.ToInt();
        }
    }
}
