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
    internal class DriverCategoryService : AbstractService<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory>,
        IDataBaseService<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO>
    {
        public DriverCategoryService(IUnitOfWorkService unitOfWorkService, IMapper mapper) : base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorDriverCategoryService(unitOfWorkService.UnitOfWorkLaborProtectionContext, Localizer);
        }

        public override void AddDataToDbAsync(DriverCategory data) => UnitOfWork.DriverCategories.AddAsync(data);
        public override void UpdateDataInDbAsync(DriverCategory data) => UnitOfWork.DriverCategories.Update(data);
        public override void DeleteDataFromDbAsync(DriverCategory data) => UnitOfWork.DriverCategories.Delete(data);
        public override Task<DriverCategory> FindDataAsync(Guid id) => UnitOfWork.DriverCategories.FindAsync(x => x.Id == id);

        public override Task<DriverCategory> FindDataIfAddAsync(DriverCategoryAddDTO modelDTO)
        {
            return UnitOfWork.DriverCategories.FindAsync(x => x.Name == modelDTO.Name);
        }

        public override async Task<List<DriverCategory>> FindPageDataAsync(int startItem, int countItem)
        {
            return await UnitOfWork.DriverCategories.GetPageAsync(startItem, countItem);
        }

        public override Task<DriverCategory> FindDataIfUpdateAsync(DriverCategoryGetUpdateDTO modelDTO)
        {
            return UnitOfWork.DriverCategories.FindAsync(x => x.Id == modelDTO.Id);
        }
    }
}
