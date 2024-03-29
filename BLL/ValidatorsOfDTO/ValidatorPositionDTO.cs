﻿using BLL.DTO.Positions;
using BLL.ValidatorsOfDTO.Abstract;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.ValidatorsOfDTO
{
    internal class ValidatorPositionDTO :
        AbstractCRUDValidatorDTO<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO, Position>
    {
        protected override string EntityAlreadyExist { get => "PositionAlreadyExist"; }
        protected override string EntityNotFound { get => "PositionNotFound"; }
        protected override string EntitiesNotFound { get => "PositionsNotFound"; }

        public ValidatorPositionDTO(IUnitOfWork<LaborProtectionContext> unitOfWork)
            : base(unitOfWork) { }

        protected override Task<Position> FindDataAsync(Guid id) =>
            UnitOfWork.Positions.FindAsync(x => x.Id == id);

        protected override Task<List<Position>> FindPageDataAsync(int startItem, int countItem) =>
            UnitOfWork.Positions.GetPageAsync(startItem, countItem);

        protected override Task<Position> FindDataAsync(PositionAddDTO modelDTO) =>
            UnitOfWork.Positions.FindAsync(x => x.Name == modelDTO.Name);
        protected override Task<int> GetCountElementAsync() => UnitOfWork.Positions.CountElementAsync();
    }
}
