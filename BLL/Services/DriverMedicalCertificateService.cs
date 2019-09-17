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
        AbstractService<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate>, 
        IDataBaseService<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO>
    {
        public DriverMedicalCertificateService(IUnitOfWorkService unitOfWorkService, IMapper mapper) : base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorDriverMedicalCertificateService(unitOfWorkService.UnitOfWorkLaborProtectionContext, Localizer);
        }

        public override void AddDataToDbAsync(DriverMedicalCertificate data) => UnitOfWork.DriverMedicalCertificates.AddAsync(data);
        public override void UpdateDataInDbAsync(DriverMedicalCertificate data) => UnitOfWork.DriverMedicalCertificates.Update(data);
        public override void DeleteDataFromDbAsync(DriverMedicalCertificate data) => UnitOfWork.DriverMedicalCertificates.Delete(data);
        public override Task<DriverMedicalCertificate> FindDataAsync(Guid id) => UnitOfWork.DriverMedicalCertificates.FindAsync(x => x.Id == id);

        public override Task<DriverMedicalCertificate> FindDataIfAddAsync(DriverMedicalCertificateAddDTO modelDTO)
        {
            return UnitOfWork.DriverMedicalCertificates.FindAsync(x => x.SerialNumber == modelDTO.SerialNumber);
        }

        public override async Task<List<DriverMedicalCertificate>> FindPageDataAsync(int startItem, int countItem)
        {
            return await UnitOfWork.DriverMedicalCertificates.GetPageAsync(startItem, countItem);
        }

        public override Task<DriverMedicalCertificate> FindDataIfUpdateAsync(DriverMedicalCertificateUpdateDTO modelDTO)
        {
            return UnitOfWork.DriverMedicalCertificates.FindAsync(x => x.Id == modelDTO.Id);
        }
    }
}
