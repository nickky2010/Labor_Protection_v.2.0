using BLL;
using BLL.DTO.Positions;
using BLL.Interfaces;
using BLL.ValidatorsOfDTO;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitTests.BLL.ValidatorsOfDTO
{
    public class ValidatorPositionDTOTest :
        AbstractCRUDValidatorDTOTest<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO, Position>
    {
        protected override IValidatorDTO<PositionAddDTO, PositionGetUpdateDTO, Position> CreateValidator
            (IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            return new ValidatorPositionDTO(unitOfWork, localizer);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.Positions.CountElementAsync();
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<Position>>> SetupFindExpression()
        {
            return a => a.Positions.FindAsync(It.IsAny<Expression<Func<Position, bool>>>());
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<List<Position>>>> SetupGetPageExpression()
        {
            return a => a.Positions.GetPageAsync(It.IsAny<int>(), It.IsAny<int>());
        }
    }
}
