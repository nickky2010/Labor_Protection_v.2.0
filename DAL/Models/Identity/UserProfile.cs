using DAL.Extentions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models.Identity
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public byte[] Photo { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public byte[] RowVersion { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is UserProfile)) return false;
            UserProfile u = (UserProfile)obj;
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
