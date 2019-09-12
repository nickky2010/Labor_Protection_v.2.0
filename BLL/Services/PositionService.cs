using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PositionService : Service, IDataBaseService<PositionDTO>
    {
        public IUnitOfWork<LaborProtectionContext> UnitOfWork { get; set; }
        public IMapper Mapper { get; set; }
        public IStringLocalizer<SharedResource> Localizer { get; set; }

        public PositionService(IUnitOfWorkService unitOfWorkService, IMapper mapper)
        {
            UnitOfWork = unitOfWorkService.UnitOfWorkLaborProtectionContext;
            Mapper = mapper;
        }

        public async Task<IAppActionResult> AddAsync(PositionDTO entity)
        {
            var position = await UnitOfWork.Positions.FindAsync(x => x.Name == entity.Name);
            if (position == null)
            {
                position = Mapper.Map<PositionDTO, Position>(entity);
                position.Employees = null;
                UnitOfWork.Positions.AddAsync(position);
                await UnitOfWork.SaveChangesAsync();
                position = await UnitOfWork.Positions.FindAsync(x => x.Name == entity.Name);
                if (position != null)
                {
                    return new AppActionResult<PositionDTO>
                    {
                        Data = Mapper.Map<Position, PositionDTO>(position),
                        Status = (int)HttpStatusCode.Created
                    };
                }
                return new AppActionResult { Status = (int)HttpStatusCode.InternalServerError, ErrorMessages = new List<string> { Localizer["AfterAddPositionNotFound"] } };
            }
            return new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["PositionAlreadyExist"] } };
        }

        public async Task<IAppActionResult> DeleteAsync(Guid guid)
        {
            var position = await UnitOfWork.Positions.FindAsync(x => x.Id == guid);
            if (position != null)
            {
                UnitOfWork.Positions.Delete(position);
                await UnitOfWork.SaveChangesAsync();
                position = await UnitOfWork.Positions.FindAsync(x => x.Id == guid);
                if (position == null)
                    return new AppActionResult { Status = (int)HttpStatusCode.OK };
                return new AppActionResult { Status = (int)HttpStatusCode.InternalServerError, ErrorMessages = new List<string> { Localizer["PositionFoundAfterDelete"] } };
            }
            return new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["PositionNotFound"] } };
        }

        public async Task<IAppActionResult> GetAsync(Guid guid)
        {
            var position = await UnitOfWork.Positions.FindAsync(x => x.Id == guid);
            if (position != null)
            {
                return new AppActionResult<PositionDTO>
                {
                    Data = Mapper.Map<Position, PositionDTO>(position),
                    Status = (int)HttpStatusCode.OK
                };
            }
            return new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["PositionNotFound"] } };
        }

        public async Task<IAppActionResult> GetPageAsync(int startItem, int countItem)
        {
            var positions = await UnitOfWork.Positions.GetPageAsync(startItem, countItem);
            if (positions != null && positions.Count != 0)
            {
                return new AppActionResult<IList<PositionDTO>>
                {
                    Data = Mapper.Map<IList<Position>, IList<PositionDTO>>(positions),
                    Status = (int)HttpStatusCode.OK
                };
            }
            return new AppActionResult { Status = (int)HttpStatusCode.NotFound, ErrorMessages = new List<string> { Localizer["PositionsNotFound"] } };
        }

        public async Task<IAppActionResult> UpdateAsync(PositionDTO model)
        {
            var data = await UnitOfWork.Positions.FindAsync(i => i.Id == model.Id);
            if (data != null)
            {
                Mapper.Map(model, data);
                data.Employees = null;
                UnitOfWork.Positions.Update(data);
                await UnitOfWork.SaveChangesAsync();
                return await GetAsync(model.Id);
            }
            return new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["PositionNotFound"] } };
        }
    }
}
