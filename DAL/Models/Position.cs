using DAL.Extentions;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Position : AbstractData<Position>
    {
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public byte[] RowVersion { get; set; }

        public Position()
        {
            Employees = new List<Employee>();
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^ 31 + Name.ToInt();
        }
    }
}
