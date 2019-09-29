using BLL;
using BLL.DTO.DriverMedicalCertificates;
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
    public class ValidatorDriverMedicalCertificateDTOTest :
        AbstractCRUDValidatorDTOWithConnectedEntitiesTest<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate>
    {
        protected override IValidatorDTO<DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate> CreateValidator
            (IUnitOfWork<LaborProtectionContext> unitOfWork, IStringLocalizer<SharedResource> localizer)
        {
            return new ValidatorDriverMedicalCertificateDTO(unitOfWork, localizer);
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<int>>> SetupCountExpression()
        {
            return a => a.DriverMedicalCertificates.CountElementAsync();
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<DriverMedicalCertificate>>> SetupFindExpression()
        {
            return a => a.DriverMedicalCertificates.FindAsync(It.IsAny<Expression<Func<DriverMedicalCertificate, bool>>>());
        }

        protected override Expression<Func<IUnitOfWork<LaborProtectionContext>, Task<List<DriverMedicalCertificate>>>> SetupGetPageExpression()
        {
            return a => a.DriverMedicalCertificates.GetPageAsync(It.IsAny<int>(), It.IsAny<int>());
        }

        protected override void SetMocksForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, DriverMedicalCertificateAddDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x => x.Employees.IsIdExistAsync(dataDTO.EmployeeId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
            unitOfWork.Setup(x => x.DriverCategories.IsAllIdExistAsync(dataDTO.DriverCategoriesId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }

        protected override void SetMocksForConnectedEntities(Mock<IUnitOfWork<LaborProtectionContext>> unitOfWork, DriverMedicalCertificateUpdateDTO dataDTO, bool IsEntitiesExist)
        {
            unitOfWork.Setup(x => x.Employees.IsIdExistAsync(dataDTO.EmployeeId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
            unitOfWork.Setup(x => x.DriverCategories.IsAllIdExistAsync(dataDTO.DriverCategoriesId))
                .ReturnsAsync(IsEntitiesExist).Verifiable();
        }
    }
}
