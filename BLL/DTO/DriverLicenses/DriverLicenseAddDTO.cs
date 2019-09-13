using BLL.Interfaces;
using DAL.Extentions;
using System;
using System.Collections.Generic;

namespace BLL.DTO.DriverLicenses
{
    public class DriverLicenseAddDTO : AbstractDataDTO<DriverLicenseAddDTO>, IAddDTO
    {
        public DateTime DateOfIssue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string SerialNumber { get; set; }
        public Guid EmployeeId { get; set; }
        public IList<Guid> DriverCategoriesId { get; set; }
        public IList<Guid> DriverLicensePhotosId { get; set; }
        public DriverLicenseAddDTO()
        {
            DriverLicensePhotosId = new List<Guid>();
            DriverCategoriesId = new List<Guid>();
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + SerialNumber.ToInt();
            hash ^= 31 + DateOfIssue.ToShortDateString().ToInt();
            return hash ^ 31 + ExpiryDate.ToShortDateString().ToInt();
        }
    }
}
