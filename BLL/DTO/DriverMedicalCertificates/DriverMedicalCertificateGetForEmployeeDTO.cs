﻿using BLL.DTO.DriverCategories;
using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.Interfaces;
using DAL.Extentions;
using System;
using System.Collections.Generic;

namespace BLL.DTO.DriverMedicalCertificates
{
    public class DriverMedicalCertificateGetForEmployeeDTO : AbstractDataDTO<DriverMedicalCertificateGetForEmployeeDTO>
    {
        public Guid Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public IList<DriverCategoryGetUpdateDTO> DriverCategories { get; set; }
        public IList<DriverMedicalCertificatePhotoGetUpdateDTO> Photos { get; set; }
        public DriverMedicalCertificateGetForEmployeeDTO()
        {
            Photos = new List<DriverMedicalCertificatePhotoGetUpdateDTO>();
            DriverCategories = new List<DriverCategoryGetUpdateDTO>();
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