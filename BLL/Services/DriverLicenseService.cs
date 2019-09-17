using AutoMapper;
using BLL.DTO.DriverLicenses;
using BLL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.ValidatorsOfServices;

namespace BLL.Services
{
    internal class DriverLicenseService : 
        AbstractService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense>, 
        IDataBaseService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO>
    {
        public DriverLicenseService(IUnitOfWorkService unitOfWorkService, IMapper mapper) : base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorDriverLicenseService(unitOfWorkService.UnitOfWorkLaborProtectionContext, Localizer);
        }

        public override void AddDataToDbAsync(DriverLicense data) => UnitOfWork.DriverLicenses.AddAsync(data);
        public override void UpdateDataInDbAsync(DriverLicense data) => UnitOfWork.DriverLicenses.Update(data);
        public override void DeleteDataFromDbAsync(DriverLicense data) => UnitOfWork.DriverLicenses.Delete(data);
        public override Task<DriverLicense> FindDataAsync(Guid id) => UnitOfWork.DriverLicenses.FindAsync(x => x.Id == id);

        public override Task<DriverLicense> FindDataIfAddAsync(DriverLicenseAddDTO modelDTO)
        {
            return UnitOfWork.DriverLicenses.FindAsync(x => x.SerialNumber == modelDTO.SerialNumber);
        }

        public override async Task<List<DriverLicense>> FindPageDataAsync(int startItem, int countItem)
        {
            return await UnitOfWork.DriverLicenses.GetPageAsync(startItem, countItem);
        }

        public override Task<DriverLicense> FindDataIfUpdateAsync(DriverLicenseUpdateDTO modelDTO)
        {
            return UnitOfWork.DriverLicenses.FindAsync(x => x.Id == modelDTO.Id);
        }
    }
}
