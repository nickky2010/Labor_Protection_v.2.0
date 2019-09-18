using AutoMapper;
using BLL.DTO.DriverCategories;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class DriverCategoryService : 
        AbstractService<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory>
    {
        public DriverCategoryService(IUnitOfWorkService unitOfWorkService, IMapper mapper) : 
            base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorDriverCategoryService(unitOfWorkService.UnitOfWorkLaborProtectionContext, Localizer);
        }

        protected override void AddDataToDbAsync(DriverCategory data) => UnitOfWork.DriverCategories.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverCategory data) => UnitOfWork.DriverCategories.Update(data);
        protected override void DeleteDataFromDbAsync(DriverCategory data) => UnitOfWork.DriverCategories.Delete(data);
        protected override Task<DriverCategory> FindDataAsync(Guid id) => UnitOfWork.DriverCategories.FindAsync(x => x.Id == id);

        protected override Task<DriverCategory> FindDataIfAddAsync(DriverCategoryAddDTO modelDTO)
        {
            return UnitOfWork.DriverCategories.FindAsync(x => x.Name == modelDTO.Name);
        }

        protected override async Task<List<DriverCategory>> FindPageDataAsync(int startItem, int countItem)
        {
            return await UnitOfWork.DriverCategories.GetPageAsync(startItem, countItem);
        }

        protected override Task<DriverCategory> FindDataIfUpdateAsync(DriverCategoryGetUpdateDTO modelDTO)
        {
            return UnitOfWork.DriverCategories.FindAsync(x => x.Id == modelDTO.Id);
        }
    }
}
