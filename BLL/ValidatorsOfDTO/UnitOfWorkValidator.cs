using BLL.DTO.DriverCategories;
using BLL.DTO.DriverLicensePhotos;
using BLL.DTO.DriverLicenses;
using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.DTO.DriverMedicalCertificates;
using BLL.DTO.Employees;
using BLL.DTO.Positions;
using BLL.Interfaces;
using BLL.ValidatorsOfDTO;
using ClosedXML.Excel;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Drawing;

namespace BLL.Services
{
    internal class UnitOfWorkValidator : IUnitOfWorkValidator
    {
        readonly IUnitOfWork<LaborProtectionContext> _unitOfWork;
        public UnitOfWorkValidator(IUnitOfWork<LaborProtectionContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IValidatorDTO<DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory> ValidatorDriverCategoryDTO =>
            new Lazy<IValidatorDTO<DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory>>(new ValidatorDriverCategoryDTO(_unitOfWork)).Value;

        public IValidatorDTO<DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense> ValidatorDriverLicenseDTO =>
            new Lazy<IValidatorDTO<DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense>>(new ValidatorDriverLicenseDTO(_unitOfWork)).Value;

        public IValidatorDTO<DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto> ValidatorDriverLicensePhotoDTO =>
            new Lazy<IValidatorDTO<DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto>>(new ValidatorDriverLicensePhotoDTO(_unitOfWork)).Value;

        public IValidatorDTO<DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate> ValidatorDriverMedicalCertificateDTO =>
            new Lazy<IValidatorDTO<DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate>>(new ValidatorDriverMedicalCertificateDTO(_unitOfWork)).Value;

        public IValidatorDTO<DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto> ValidatorDriverMedicalCertificatePhotoDTO =>
            new Lazy<IValidatorDTO<DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto>>(new ValidatorDriverMedicalCertificatePhotoDTO(_unitOfWork)).Value;

        public IValidatorDTO<EmployeeAddDTO, EmployeeUpdateDTO, Employee> ValidatorEmployeeDTO =>
            new Lazy<IValidatorDTO<EmployeeAddDTO, EmployeeUpdateDTO, Employee>>(new ValidatorEmployeeDTO(_unitOfWork)).Value;

        public IValidatorDTO<PositionAddDTO, PositionGetUpdateDTO, Position> ValidatorPositionDTO =>
            new Lazy<IValidatorDTO<PositionAddDTO, PositionGetUpdateDTO, Position>>(new ValidatorPositionDTO(_unitOfWork)).Value;

        public IValidatorOfUploadFile<XLWorkbook> ValidatorExcelFile =>
            new Lazy<IValidatorOfUploadFile<XLWorkbook>>(new ValidatorExcelFile()).Value;

        public IValidatorOfUploadFile<Image> ValidatorPhotoFile =>
            new Lazy<IValidatorOfUploadFile<Image>>(new ValidatorPhotoFile()).Value;
    }
}
