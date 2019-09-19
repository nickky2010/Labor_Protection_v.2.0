using AutoMapper;
using BLL.DTO.DriverLicensePhotos;
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
    internal class DriverLicensePhotoService : 
        AbstractCRUDDataBaseService<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO, DriverLicensePhoto>
    {
        public DriverLicensePhotoService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorDriverLicensePhotoService(unitOfWorkService.UnitOfWorkLaborProtectionContext);
        }
        protected override void AddDataToDbAsync(DriverLicensePhoto data) => UnitOfWork.DriverLicensePhotos.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverLicensePhoto data) => UnitOfWork.DriverLicensePhotos.Update(data);
        protected override void DeleteDataFromDbAsync(DriverLicensePhoto data) => UnitOfWork.DriverLicensePhotos.Delete(data);
        protected override Task<DriverLicensePhoto> FindDataAsync(Guid id) => UnitOfWork.DriverLicensePhotos.FindAsync(x => x.Id == id);

        protected override async Task<DriverLicensePhoto> FindDataIfAddAsync(DriverLicensePhotoAddDTO modelDTO)
        {
            return await UnitOfWork.DriverLicensePhotos.FindAsync(x=>x.DriverLicenseId != modelDTO.DriverLicenseId);
        }

        protected override async Task<List<DriverLicensePhoto>> FindPageDataAsync(int startItem, int countItem)
        {
            return await UnitOfWork.DriverLicensePhotos.GetPageAsync(startItem, countItem);
        }

        protected override Task<DriverLicensePhoto> FindDataIfUpdateAsync(DriverLicensePhotoUpdateDTO modelDTO)
        {
            return UnitOfWork.DriverLicensePhotos.FindAsync(x => x.Id == modelDTO.Id);
        }
        public override async Task<IAppActionResult<DriverLicensePhotoGetDTO>> AddAsync(DriverLicensePhotoAddDTO modelDTO)
        {
            var result = await Validator.ValidateAdd(null, modelDTO, HttpStatusCode.BadRequest, HttpStatusCode.OK, Localizer);
            if (!result.IsSuccess)
                return result;
            var data = Mapper.Map<DriverLicensePhotoAddDTO, DriverLicensePhoto>(modelDTO);
            AddDataToDbAsync(data);
            await UnitOfWork.SaveChangesAsync();
            data = await FindDataAsync(data.Id);
            result = Validator.ValidateDataFromDb(data, HttpStatusCode.InternalServerError, HttpStatusCode.Created, Localizer);
            if (!result.IsSuccess)
                return result;
            result.Data = Mapper.Map<DriverLicensePhoto, DriverLicensePhotoGetDTO>(data);
            return result;
        }
    }
}
