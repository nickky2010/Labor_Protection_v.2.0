using AutoMapper;
using BLL.DTO.DriverLicensePhotos;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class DriverLicensePhotoService :
        AbstractCRUDDataBaseService<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto>
    {
        protected override IValidatorDTO<DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto> Validator { get; set; }

        public DriverLicensePhotoService(IUnitOfWorkService unitOfWorkService, IMapper mapper, IUnitOfWorkValidator unitOfWorkValidator) :
            base(unitOfWorkService, mapper)
        {
            Validator = unitOfWorkValidator.ValidatorDriverLicensePhotoDTO;
            Validator.Localizer = Localizer;
        }
        protected override void AddDataToDbAsync(DriverLicensePhoto data) => UnitOfWork.DriverLicensePhotos.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverLicensePhoto data) => UnitOfWork.DriverLicensePhotos.Update(data);
        protected override void DeleteDataFromDbAsync(DriverLicensePhoto data) => UnitOfWork.DriverLicensePhotos.Delete(data);
        protected override Task<DriverLicensePhoto> FindDataAsync(Guid id) => UnitOfWork.DriverLicensePhotos.FindAsync(x => x.Id == id);
    }
}
