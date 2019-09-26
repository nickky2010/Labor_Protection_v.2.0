using BLL.DTO.Positions;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using BLL.ValidatorsOfDTO.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorPositionDTO: 
        AbstractValidatorDTO<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO, Position>
    {
        protected override string EntityAlreadyExist { get => "PositionAlreadyExist"; }
        protected override string EntityNotFound { get => "PositionNotFound"; }
        protected override string EntitiesNotFound { get => "PositionsNotFound"; }

        public ValidatorPositionDTO(IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
            : base(unitOfWork, localizer) { }

        protected override Task<Position> FindDataAsync(Guid id) =>
            UnitOfWork.Positions.FindAsync(x => x.Id == id);

        protected override Task<List<Position>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.Positions.GetPageAsync(startItem, countItem);

        protected override Task<Position> FindDataAsync(PositionAddDTO modelDTO) =>
            UnitOfWork.Positions.FindAsync(x => x.Name == modelDTO.Name);
        protected override Task<int> GetCountElementAsync() => UnitOfWork.Positions.CountElementAsync();
    }
}
