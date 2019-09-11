using BLL.Interfaces;
using DAL.Extentions;

namespace BLL.DTO.Identity
{
    public class UserDTO : IDataDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public RoleDTO Role { get; set; }
        public UserProfileDTO UserProfile { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is UserDTO)) return false;
            UserDTO u = (UserDTO)obj;
            return GetHashCode() == u.GetHashCode();
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
