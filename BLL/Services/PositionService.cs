using AutoMapper;
using BLL.DTO.Positions;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class PositionService :
        AbstractCRUDDataBaseService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO, Position>
    {
        protected override IValidatorDTO<PositionAddDTO, PositionGetUpdateDTO, Position> Validator { get; set; }

        public PositionService(IUnitOfWorkService unitOfWorkService, IMapper mapper, IUnitOfWorkValidator unitOfWorkValidator) :
            base(unitOfWorkService, mapper)
        {
            Validator = unitOfWorkValidator.ValidatorPositionDTO;
            Validator.Localizer = Localizer;
        }

        protected override void AddDataToDbAsync(Position data) => UnitOfWork.Positions.AddAsync(data);
        protected override void UpdateDataInDbAsync(Position data) => UnitOfWork.Positions.Update(data);
        protected override void DeleteDataFromDbAsync(Position data) => UnitOfWork.Positions.Delete(data);
        protected override Task<Position> FindDataAsync(Guid id) => UnitOfWork.Positions.FindAsync(x => x.Id == id);
    }
}
