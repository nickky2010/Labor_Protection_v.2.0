﻿using DAL.Extentions;
using DAL.Interfaces;
using System;

namespace DAL.Models
{
    public class DriverLicensePhoto : AbstractData<DriverLicensePhoto>, IPhotoData
    {
        public Guid DriverLicenseId { get; set; }
        public virtual DriverLicense DriverLicense { get; set; }
        public byte[] Photo { get; set; }
        public override int GetHashCode()
        {
            int hash = 17;
            hash ^= 31 + Id.ToString().ToInt();
            return hash ^= 31 + DriverLicenseId.ToString().ToInt();
        }
    }
}
