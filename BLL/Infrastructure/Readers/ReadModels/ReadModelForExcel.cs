using BLL.Interfaces;
using DAL.Models;
using System.Collections.Generic;

namespace BLL.Infrastructure.Readers.ReadModels
{
    public class ReadModelForExcel : IReadModel
    {
        public List<Employee> Employees { get; set; }
        public List<Position> Positions { get; set; }

        public ReadModelForExcel()
        {
            Employees = new List<Employee>();
            Positions = new List<Position>();
        }
    }
}
