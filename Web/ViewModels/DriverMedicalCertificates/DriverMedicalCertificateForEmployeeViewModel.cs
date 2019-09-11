using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Interfaces;

namespace Web.ViewModels.DriverMedicalCertificates
{
    public class DriverMedicalCertificateForEmployeeViewModel : IViewModel
    {
        //[Display(Name = "DateOfIssue", ResourceType = typeof(SharedResource))]
        //[Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "EnterDateOfIssue")]
        public DateTime DateOfIssue { get; set; }

        //[Display(Name = "ExpiryDate", ResourceType = typeof(SharedResource))]
        //[Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "EnterExpiryDate")]
        public DateTime ExpiryDate { get; set; }

        //[Display(Name = "SerialNumber", ResourceType = typeof(SharedResource))]
        //[Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "EnterSerialNumber")]
        public string SerialNumber { get; set; }

        //[Display(Name = "OpenCategories", ResourceType = typeof(SharedResource))]
        //[Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "EnterOpenCategories")]
        public ICollection<string> OpenCategories { get; set; }

        //[Display(Name = "DriverMedicalCertificatePhotos", ResourceType = typeof(SharedResource))]
        //[Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = "EnterDriverMedicalCertificatePhotos")]
        public ICollection<byte[]> CertificatePhotos { get; set; }
    }
}