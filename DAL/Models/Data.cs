using System;

namespace DAL.Models
{
    public class Data
    {
        public Guid Id { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
