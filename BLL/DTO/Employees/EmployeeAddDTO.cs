﻿using BLL.Interfaces;
using DAL.Extentions;
using System;

namespace BLL.DTO.Employees
{
    public class EmployeeAddDTO : AbstractDataDTO<EmployeeAddDTO>, IAddDTO
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public Guid PositionId { get; set; }

        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + PositionId.ToString().ToInt();
            hash ^= 31 + Surname.ToInt();
            hash ^= 31 + FirstName.ToInt();
            return hash ^ 31 + Patronymic.ToInt();
        }
    }
}
