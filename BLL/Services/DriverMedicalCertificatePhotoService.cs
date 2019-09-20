using AutoMapper;
using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using DAL.Models;
using System;
using System.Threading.Tasks;
using BLL.Services.Abstract;

namespace BLL.Services
{
    internal class DriverMedicalCertificatePhotoService : 
        AbstractCRUDDataBaseService<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto>
    {
        public DriverMedicalCertificatePhotoService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorDriverMedicalCertificatePhotoDTO(unitOfWorkService.UnitOfWorkLaborProtectionContext, Localizer);
        }
        protected override void AddDataToDbAsync(DriverMedicalCertificatePhoto data) => UnitOfWork.DriverMedicalCertificatePhotos.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverMedicalCertificatePhoto data) => UnitOfWork.DriverMedicalCertificatePhotos.Update(data);
        protected override void DeleteDataFromDbAsync(DriverMedicalCertificatePhoto data) => UnitOfWork.DriverMedicalCertificatePhotos.Delete(data);
        protected override Task<DriverMedicalCertificatePhoto> FindDataAsync(Guid id) => UnitOfWork.DriverMedicalCertificatePhotos.FindAsync(x => x.Id == id);
    }
}
