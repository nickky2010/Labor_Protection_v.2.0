using AutoMapper;
using BLL.DTO.DriverLicenses;
using BLL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.ValidatorsOfServices;
using BLL.Services.Abstract;

namespace BLL.Services
{
    internal class DriverLicenseService : 
        AbstractCRUDDataBaseService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense>
    {
        public DriverLicenseService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorDriverLicenseService(unitOfWorkService.UnitOfWorkLaborProtectionContext);
        }

        protected override void AddDataToDbAsync(DriverLicense data) => UnitOfWork.DriverLicenses.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverLicense data) => UnitOfWork.DriverLicenses.Update(data);
        protected override void DeleteDataFromDbAsync(DriverLicense data) => UnitOfWork.DriverLicenses.Delete(data);
        protected override Task<DriverLicense> FindDataAsync(Guid id) => UnitOfWork.DriverLicenses.FindAsync(x => x.Id == id);

        protected override Task<DriverLicense> FindDataIfAddAsync(DriverLicenseAddDTO modelDTO)
        {
            return UnitOfWork.DriverLicenses.FindAsync(x => x.SerialNumber == modelDTO.SerialNumber);
        }

        protected override async Task<List<DriverLicense>> FindPageDataAsync(int startItem, int countItem)
        {
            return await UnitOfWork.DriverLicenses.GetPageAsync(startItem, countItem);
        }

        protected override Task<DriverLicense> FindDataIfUpdateAsync(DriverLicenseUpdateDTO modelDTO)
        {
            return UnitOfWork.DriverLicenses.FindAsync(x => x.Id == modelDTO.Id);
        }
    }
}
