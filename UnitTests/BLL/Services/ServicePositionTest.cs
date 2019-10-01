using AutoMapper;
using BLL;
using BLL.DTO.Positions;
using BLL.Interfaces;
using BLL.Services;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitTests.BLL.Services.AbstractServicesTest;

namespace UnitTests.BLL.Services
{
    public class ServicePositionTest :
        AbstractCRUDServiceTest<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO, Position>
    {
        protected override ICRUDDataBaseService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO> CreateService
            (IUnitOfWorkService unitOfWorkService, IMapper mapper, IStringLocalizer<SharedResource> localizer, IUnitOfWorkValidator unitOfWorkValidator)
        {
            var service = new PositionService(unitOfWorkService, mapper, unitOfWorkValidator);
            service.Localizer = localizer;
            return service;
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task>> SetupAddExpression(Position data)
        {
            return a => a.Positions.AddAsync(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.Positions.CountElementAsync();
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupDeleteExpression(Position data)
        {
            return a => a.Positions.Delete(data);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<Position>>> SetupFindExpression()
        {
            return a => a.Positions.FindAsync(It.IsAny<Expression<Func<Position, bool>>>());
        }

        protected override Expression<Action<IUnitOfWork<LaborProtectionContext>>> SetupUpdateExpression(Position data)
        {
            return a => a.Positions.Update(data);
        }

        protected override Expression<Func<IUnitOfWorkValidator, IValidatorDTO<PositionAddDTO, PositionGetUpdateDTO, Position>>> SetupValidatorExpression()
        {
            return a => a.ValidatorPositionDTO;
        }
    }
}
