using BLL.Interfaces;
using DAL.Extentions;

namespace BLL.DTO.Identity
{
    public class UserProfileDTO : IDataDTO
    {
        public string Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public byte[] Photo { get; set; }
        public UserDTO User { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is UserProfileDTO)) return false;
            UserProfileDTO u = (UserProfileDTO)obj;
            return GetHashCode() == u.GetHashCode();
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToInt();
            hash ^= 31 + FirstName.ToInt();
            hash ^= 31 + Surname.ToInt();
            return hash ^ 31 + Patronymic.ToInt();
        }
    }
}
