using BLL.DTO.DriverCategories;
using BLL.DTO.DriverLicensePhotos;
using BLL.DTO.DriverLicenses;
using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.DTO.DriverMedicalCertificates;
using BLL.DTO.Employees;
using BLL.DTO.Positions;
using ClosedXML.Excel;
using DAL.Models;
using System.Drawing;

namespace BLL.Interfaces
{
    public interface IUnitOfWorkValidator
    {
        IValidatorDTO<DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory> ValidatorDriverCategoryDTO { get; }
        IValidatorDTO<DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense> ValidatorDriverLicenseDTO { get; }
        IValidatorDTO<DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto> ValidatorDriverLicensePhotoDTO { get; }
        IValidatorDTO<DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate> ValidatorDriverMedicalCertificateDTO { get; }
        IValidatorDTO<DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto> ValidatorDriverMedicalCertificatePhotoDTO { get; }
        IValidatorDTO<EmployeeAddDTO, EmployeeUpdateDTO, Employee> ValidatorEmployeeDTO { get; }
        IValidatorDTO<PositionAddDTO, PositionGetUpdateDTO, Position> ValidatorPositionDTO { get; }
        IValidatorOfUploadFile<XLWorkbook> ValidatorExcelFile { get; }
        IValidatorOfUploadFile<Image> ValidatorPhotoFile { get; }

    }
}
