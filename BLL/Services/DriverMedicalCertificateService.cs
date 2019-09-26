using AutoMapper;
using BLL.DTO.DriverMedicalCertificates;
using BLL.Interfaces;
using BLL.Services.Abstract;
using BLL.ValidatorsOfDTO;
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class DriverMedicalCertificateService :
        AbstractCRUDDataBaseService<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate>
    {
        public DriverMedicalCertificateService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorDriverMedicalCertificateDTO(unitOfWorkService.UnitOfWorkLaborProtectionContext, Localizer);
        }

        protected override void AddDataToDbAsync(DriverMedicalCertificate data) => UnitOfWork.DriverMedicalCertificates.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverMedicalCertificate data) => UnitOfWork.DriverMedicalCertificates.Update(data);
        protected override void DeleteDataFromDbAsync(DriverMedicalCertificate data) => UnitOfWork.DriverMedicalCertificates.Delete(data);
        protected override Task<DriverMedicalCertificate> FindDataAsync(Guid id) => UnitOfWork.DriverMedicalCertificates.FindAsync(x => x.Id == id);
    }
}
