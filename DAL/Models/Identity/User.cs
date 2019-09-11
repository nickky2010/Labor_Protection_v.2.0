using DAL.Extentions;
using Microsoft.AspNetCore.Identity;

namespace DAL.Models.Identity
{
    public class User : IdentityUser
    {
        public string RoleId { get; set; }
        public virtual Role Role { get; set; }
        public string UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public byte[] RowVersion { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is User)) return false;
            User u = (User)obj;
            return Id == u.Id;
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToInt();
            hash ^= 31 + Email.ToString().ToInt();
            return hash ^ 31 + UserName.ToInt();
        }
    }
}
