﻿using AutoMapper;
using BLL.DTO.Positions;
using BLL.Interfaces;
using BLL.ValidatorsOfServices;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class PositionService : 
        AbstractService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO, Position>
    {
        public PositionService(IUnitOfWorkService unitOfWorkService, IMapper mapper) :
            base(unitOfWorkService, mapper)
        {
            Validator = new ValidatorPositionService(unitOfWorkService.UnitOfWorkLaborProtectionContext, Localizer);
        }

        protected override void AddDataToDbAsync(Position data) => UnitOfWork.Positions.AddAsync(data);
        protected override void UpdateDataInDbAsync(Position data) => UnitOfWork.Positions.Update(data);
        protected override void DeleteDataFromDbAsync(Position data) => UnitOfWork.Positions.Delete(data);
        protected override Task<Position> FindDataAsync(Guid id) => UnitOfWork.Positions.FindAsync(x => x.Id == id);

        protected override Task<Position> FindDataIfAddAsync(PositionAddDTO modelDTO)
        {
            return UnitOfWork.Positions.FindAsync(x => x.Name == modelDTO.Name);
        }

        protected override async Task<List<Position>> FindPageDataAsync(int startItem, int countItem)
        {
            return await UnitOfWork.Positions.GetPageAsync(startItem, countItem);
        }

        protected override Task<Position> FindDataIfUpdateAsync(PositionGetUpdateDTO modelDTO)
        {
            return UnitOfWork.Positions.FindAsync(x => x.Id == modelDTO.Id);
        }
    }
}
