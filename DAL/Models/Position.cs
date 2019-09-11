using DAL.Extentions;
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Position
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public Position()
        {
            Employees = new List<Employee>();
        }

        public byte[] RowVersion { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Position)) return false;
            Position e = (Position)obj;
            return Id.Equals(e.Id);
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^ 31 + Name.ToInt();
        }
    }
}
