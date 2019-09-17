using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.EF
{
    public class EFDriverCategoryRepository : AbstractEFRepository<DriverCategory>
    {
        public EFDriverCategoryRepository(DbContext context) : base(context)
        {
            Query = Query
                .Include(b => b.DriverLicenseDriverCategories)
                .Include(b => b.DriverMedicalCertificateDriverCategories);
        }
    }
}