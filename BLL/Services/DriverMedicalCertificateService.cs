using AutoMapper;
using BLL.DTO.DriverMedicalCertificates;
using BLL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.ValidatorsOfServices;

namespace BLL.Services
{
    internal class DriverMedicalCertificateService : 
        AbstractService<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate> 
    {
        public DriverMedicalCertificateService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorDriverMedicalCertificateService(unitOfWorkService.UnitOfWorkLaborProtectionContext, Localizer);
        }

        protected override void AddDataToDbAsync(DriverMedicalCertificate data) => UnitOfWork.DriverMedicalCertificates.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverMedicalCertificate data) => UnitOfWork.DriverMedicalCertificates.Update(data);
        protected override void DeleteDataFromDbAsync(DriverMedicalCertificate data) => UnitOfWork.DriverMedicalCertificates.Delete(data);
        protected override Task<DriverMedicalCertificate> FindDataAsync(Guid id) => UnitOfWork.DriverMedicalCertificates.FindAsync(x => x.Id == id);

        protected override Task<DriverMedicalCertificate> FindDataIfAddAsync(DriverMedicalCertificateAddDTO modelDTO)
        {
            return UnitOfWork.DriverMedicalCertificates.FindAsync(x => x.SerialNumber == modelDTO.SerialNumber);
        }

        protected override async Task<List<DriverMedicalCertificate>> FindPageDataAsync(int startItem, int countItem)
        {
            return await UnitOfWork.DriverMedicalCertificates.GetPageAsync(startItem, countItem);
        }

        protected override Task<DriverMedicalCertificate> FindDataIfUpdateAsync(DriverMedicalCertificateUpdateDTO modelDTO)
        {
            return UnitOfWork.DriverMedicalCertificates.FindAsync(x => x.Id == modelDTO.Id);
        }
    }
}
