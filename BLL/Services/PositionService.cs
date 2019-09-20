using AutoMapper;
using BLL.DTO.Positions;
using BLL.Interfaces;
using BLL.ValidatorsOfDTO;
using DAL.Models;
using System;
using System.Threading.Tasks;
using BLL.Services.Abstract;

namespace BLL.Services
{
    internal class PositionService : 
        AbstractCRUDDataBaseService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO, Position>
    {
        public PositionService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorPositionDTO(unitOfWorkService.UnitOfWorkLaborProtectionContext, Localizer);
        }

        protected override void AddDataToDbAsync(Position data) => UnitOfWork.Positions.AddAsync(data);
        protected override void UpdateDataInDbAsync(Position data) => UnitOfWork.Positions.Update(data);
        protected override void DeleteDataFromDbAsync(Position data) => UnitOfWork.Positions.Delete(data);
        protected override Task<Position> FindDataAsync(Guid id) => UnitOfWork.Positions.FindAsync(x => x.Id == id);
    }
}
