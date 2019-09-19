using AutoMapper;
using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Net;
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
            Validator = new ValidatorDriverMedicalCertificatePhotoService(unitOfWorkService.UnitOfWorkLaborProtectionContext);
        }
        protected override void AddDataToDbAsync(DriverMedicalCertificatePhoto data) => UnitOfWork.DriverMedicalCertificatePhotos.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverMedicalCertificatePhoto data) => UnitOfWork.DriverMedicalCertificatePhotos.Update(data);
        protected override void DeleteDataFromDbAsync(DriverMedicalCertificatePhoto data) => UnitOfWork.DriverMedicalCertificatePhotos.Delete(data);
        protected override Task<DriverMedicalCertificatePhoto> FindDataAsync(Guid id) => UnitOfWork.DriverMedicalCertificatePhotos.FindAsync(x => x.Id == id);

        protected override async Task<DriverMedicalCertificatePhoto> FindDataIfAddAsync(DriverMedicalCertificatePhotoAddDTO modelDTO)
        {
            return await UnitOfWork.DriverMedicalCertificatePhotos.FindAsync(x=>x.DriverMedicalCertificateId != modelDTO.DriverMedicalCertificateId);
        }

        protected override async Task<List<DriverMedicalCertificatePhoto>> FindPageDataAsync(int startItem, int countItem)
        {
            return await UnitOfWork.DriverMedicalCertificatePhotos.GetPageAsync(startItem, countItem);
        }

        protected override Task<DriverMedicalCertificatePhoto> FindDataIfUpdateAsync(DriverMedicalCertificatePhotoUpdateDTO modelDTO)
        {
            return UnitOfWork.DriverMedicalCertificatePhotos.FindAsync(x => x.Id == modelDTO.Id);
        }
        public override async Task<IAppActionResult<DriverMedicalCertificatePhotoGetDTO>> AddAsync(DriverMedicalCertificatePhotoAddDTO modelDTO)
        {
            var result = await Validator.ValidateAdd(null, modelDTO, HttpStatusCode.BadRequest, HttpStatusCode.OK, Localizer);
            if (!result.IsSuccess)
                return result;
            var data = Mapper.Map<DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhoto>(modelDTO);
            AddDataToDbAsync(data);
            await UnitOfWork.SaveChangesAsync();
            data = await FindDataAsync(data.Id);
            result = Validator.ValidateDataFromDb(data, HttpStatusCode.InternalServerError, HttpStatusCode.Created, Localizer);
            if (!result.IsSuccess)
                return result;
            result.Data = Mapper.Map<DriverMedicalCertificatePhoto, DriverMedicalCertificatePhotoGetDTO>(data);
            return result;
        }
    }
}
