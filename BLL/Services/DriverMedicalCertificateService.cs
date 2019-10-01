using AutoMapper;
using BLL.DTO.DriverMedicalCertificates;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class DriverMedicalCertificateService :
        AbstractCRUDDataBaseService<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate>
    {
        protected override IValidatorDTO<DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate> Validator { get; set; }

        public DriverMedicalCertificateService(IUnitOfWorkService unitOfWorkService, IMapper mapper, IUnitOfWorkValidator unitOfWorkValidator) :
            base(unitOfWorkService, mapper)
        {
            Validator = unitOfWorkValidator.ValidatorDriverMedicalCertificateDTO;
            Validator.Localizer = Localizer;
        }

        protected override void AddDataToDbAsync(DriverMedicalCertificate data) => UnitOfWork.DriverMedicalCertificates.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverMedicalCertificate data) => UnitOfWork.DriverMedicalCertificates.Update(data);
        protected override void DeleteDataFromDbAsync(DriverMedicalCertificate data) => UnitOfWork.DriverMedicalCertificates.Delete(data);
        protected override Task<DriverMedicalCertificate> FindDataAsync(Guid id) => UnitOfWork.DriverMedicalCertificates.FindAsync(x => x.Id == id);
    }
}
