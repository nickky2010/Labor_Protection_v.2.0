using AutoMapper;
using BLL.DTO.DriverLicenses;
using BLL.Interfaces;
using BLL.Services.Abstract;
using BLL.ValidatorsOfDTO;
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class DriverLicenseService :
        AbstractCRUDDataBaseService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense>
    {
        public DriverLicenseService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorDriverLicenseDTO(unitOfWorkService.UnitOfWorkLaborProtectionContext, Localizer);
        }

        protected override void AddDataToDbAsync(DriverLicense data) => UnitOfWork.DriverLicenses.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverLicense data) => UnitOfWork.DriverLicenses.Update(data);
        protected override void DeleteDataFromDbAsync(DriverLicense data) => UnitOfWork.DriverLicenses.Delete(data);
        protected override Task<DriverLicense> FindDataAsync(Guid id) => UnitOfWork.DriverLicenses.FindAsync(x => x.Id == id);
    }
}
