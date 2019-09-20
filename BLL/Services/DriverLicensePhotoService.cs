using AutoMapper;
using BLL.DTO.DriverLicensePhotos;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using DAL.Models;
using System;
using System.Threading.Tasks;
using BLL.Services.Abstract;

namespace BLL.Services
{
    internal class DriverLicensePhotoService : 
        AbstractCRUDDataBaseService<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto>
    {
        public DriverLicensePhotoService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorDriverLicensePhotoDTO(unitOfWorkService.UnitOfWorkLaborProtectionContext, Localizer);
        }
        protected override void AddDataToDbAsync(DriverLicensePhoto data) => UnitOfWork.DriverLicensePhotos.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverLicensePhoto data) => UnitOfWork.DriverLicensePhotos.Update(data);
        protected override void DeleteDataFromDbAsync(DriverLicensePhoto data) => UnitOfWork.DriverLicensePhotos.Delete(data);
        protected override Task<DriverLicensePhoto> FindDataAsync(Guid id) => UnitOfWork.DriverLicensePhotos.FindAsync(x => x.Id == id);
    }
}
