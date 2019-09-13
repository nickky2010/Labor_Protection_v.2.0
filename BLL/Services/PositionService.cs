using AutoMapper;
using BLL.DTO.Positions;
using BLL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class PositionService : AbstractService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>,
        IDataBaseService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>
    {
        public PositionService(IUnitOfWorkService unitOfWorkService, IMapper mapper) : base(unitOfWorkService, mapper) { }

        public async Task<IAppActionResult<PositionGetUpdateDTO>> AddAsync(PositionAddDTO dtoAdd)
        {
            var data = await UnitOfWork.Positions.FindAsync(x => x.Name == dtoAdd.Name);
            if (data != null)
                return SendGetError(HttpStatusCode.BadRequest, "PositionAlreadyExist");
            data = Mapper.Map<Position>(dtoAdd);
            data.Id = Guid.NewGuid();
            await UnitOfWork.Positions.AddAsync(data);
            if (await UnitOfWork.SaveChangesAsync() == 0)
                return SendGetError(HttpStatusCode.InternalServerError, "DataNotWrittenToDB");
            return SendData(Mapper.Map<PositionGetUpdateDTO>(data), HttpStatusCode.Created);
        }

        public async Task<IAppActionResult> DeleteAsync(Guid guid)
        {
            var data = await UnitOfWork.Positions.FindAsync(x => x.Id == guid);
            if (data == null)
                return SendError(HttpStatusCode.BadRequest, "PositionNotFound");
            UnitOfWork.Positions.Delete(data);
            if (await UnitOfWork.SaveChangesAsync() == 0)
                return SendError(HttpStatusCode.InternalServerError, "DataNotDeletedFromDB");
            return SendResult(HttpStatusCode.OK);
        }

        public async Task<IAppActionResult<PositionGetUpdateDTO>> GetAsync(Guid guid)
        {
            var data = await UnitOfWork.Positions.FindAsync(x => x.Id == guid);
            if (data == null)
                return SendGetError(HttpStatusCode.BadRequest, "PositionNotFound");
            return SendData(Mapper.Map<Position, PositionGetUpdateDTO>(data), HttpStatusCode.OK);
        }

        public async Task<IAppActionResult<IList<PositionGetUpdateDTO>>> GetPageAsync(int startItem, int countItem)
        {
            var data = await UnitOfWork.Positions.GetPageAsync(startItem, countItem);
            if (data == null)
                return SendForListGetError(HttpStatusCode.NotFound, "PositionsNotFound");
            return SendListData(Mapper.Map<IList<Position>, List<PositionGetUpdateDTO>>(data), HttpStatusCode.OK);
        }

        public async Task<IAppActionResult<PositionGetUpdateDTO>> UpdateAsync(PositionGetUpdateDTO model)
        {
            var data = await UnitOfWork.Positions.FindAsync(i => i.Id == model.Id);
            if (data == null)
                return SendUpdateError(HttpStatusCode.NotFound, "PositionNotFound");
            Mapper.Map(model, data);
            UnitOfWork.Positions.Update(data);
            if (await UnitOfWork.SaveChangesAsync() == 0)
                return SendUpdateError(HttpStatusCode.InternalServerError, "DataNotUpdatedInDB");
            return SendUpdateData(Mapper.Map<Position, PositionGetUpdateDTO>(data), HttpStatusCode.OK);
        }
    }
}
