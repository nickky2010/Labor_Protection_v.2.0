using AutoMapper;
using BLL.DTO.DriverLicenses;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class DriverLicenseService :
        AbstractCRUDDataBaseService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense>
    {
        protected override IValidatorDTO<DriverLicenseAddDTO, DriverLicenseUpdateDTO, DriverLicense> Validator { get; set; }

        public DriverLicenseService(IUnitOfWorkService unitOfWorkService, IMapper mapper, IUnitOfWorkValidator unitOfWorkValidator) :
            base(unitOfWorkService, mapper)
        {
            Validator = unitOfWorkValidator.ValidatorDriverLicenseDTO;
            Validator.Localizer = Localizer;
        }

        protected override void AddDataToDbAsync(DriverLicense data) => UnitOfWork.DriverLicenses.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverLicense data) => UnitOfWork.DriverLicenses.Update(data);
        protected override void DeleteDataFromDbAsync(DriverLicense data) => UnitOfWork.DriverLicenses.Delete(data);
        protected override Task<DriverLicense> FindDataAsync(Guid id) => UnitOfWork.DriverLicenses.FindAsync(x => x.Id == id);
    }
}
