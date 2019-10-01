using AutoMapper;
using BLL.DTO.DriverCategories;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class DriverCategoryService :
        AbstractCRUDDataBaseService<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory>
    {
        protected override IValidatorDTO<DriverCategoryAddDTO, DriverCategoryGetUpdateDTO, DriverCategory> Validator { get; set; }

        public DriverCategoryService(IUnitOfWorkService unitOfWorkService, IMapper mapper, IUnitOfWorkValidator unitOfWorkValidator) :
            base(unitOfWorkService, mapper)
        {
            Validator = unitOfWorkValidator.ValidatorDriverCategoryDTO;
            Validator.Localizer = Localizer;
        }

        protected override void AddDataToDbAsync(DriverCategory data) => UnitOfWork.DriverCategories.AddAsync(data);
        protected override void UpdateDataInDbAsync(DriverCategory data) => UnitOfWork.DriverCategories.Update(data);
        protected override void DeleteDataFromDbAsync(DriverCategory data) => UnitOfWork.DriverCategories.Delete(data);
        protected override Task<DriverCategory> FindDataAsync(Guid id) => UnitOfWork.DriverCategories.FindAsync(x => x.Id == id);
    }
}
