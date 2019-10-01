using AutoMapper;
using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class DriverMedicalCertificatePhotoService :
        AbstractCRUDDataBaseService<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto>
    {
        protected override IValidatorDTO<DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO, DriverMedicalCertificatePhoto> Validator { get; set; }

        public DriverMedicalCertificatePhotoService(IUnitOfWorkService unitOfWorkService, IMapper mapper, IUnitOfWorkValidator unitOfWorkValidator) :
            base(unitOfWorkService, mapper)
        {
            Validator = unitOfWorkValidator.ValidatorDriverMedicalCertificatePhotoDTO;
            Validator.Localizer = Localizer;
        }
        protected override void AddDataToDbAsync(DriverMedicalCertificatePhoto data) => UnitOfWork.DriverMedicalCertificatePhotos.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverMedicalCertificatePhoto data) => UnitOfWork.DriverMedicalCertificatePhotos.Update(data);
        protected override void DeleteDataFromDbAsync(DriverMedicalCertificatePhoto data) => UnitOfWork.DriverMedicalCertificatePhotos.Delete(data);
        protected override Task<DriverMedicalCertificatePhoto> FindDataAsync(Guid id) => UnitOfWork.DriverMedicalCertificatePhotos.FindAsync(x => x.Id == id);
    }
}
