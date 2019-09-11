﻿using BLL.Interfaces;
using DAL.Extentions;
using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class DriverLicenseDTO : IDataDTO
    {
        public Guid Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public ICollection<string> OpenCategories { get; set; }
        public ICollection<byte[]> DriverLicensePhotos { get; set; }
        public EmployeeDTO Employee { get; set; }
        public DriverLicenseDTO()
        {
            DriverLicensePhotos = new List<byte[]>();
            OpenCategories = new List<string>();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DriverLicenseDTO)) return false;
            DriverLicenseDTO d = (DriverLicenseDTO)obj;
            return GetHashCode() == d.GetHashCode();
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            hash ^= 31 + SerialNumber.ToInt();
            hash ^= 31 + DateOfIssue.ToShortDateString().ToInt();
            return hash ^ 31 + ExpiryDate.ToShortDateString().ToInt();
        }
    }
}
