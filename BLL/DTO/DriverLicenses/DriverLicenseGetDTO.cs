using BLL.DTO.DriverCategories;
using BLL.DTO.DriverLicensePhotos;
using BLL.DTO.Employees;
using BLL.Interfaces;
using DAL.Extentions;
using System;
using System.Collections.Generic;

namespace BLL.DTO.DriverLicenses
{
    public class DriverLicenseGetDTO : AbstractDataDTO<DriverLicenseGetDTO>, IGetDTO
    {
        public Guid Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public EmployeeForModelsDTO Employee { get; set; }
        public IList<DriverCategoryGetUpdateDTO> DriverCategories { get; set; }
        public IList<DriverLicensePhotoGetDTO> Photos { get; set; }
        public DriverLicenseGetDTO()
        {
            Photos = new List<DriverLicensePhotoGetDTO>();
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
