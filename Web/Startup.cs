using AutoMapper;
using BLL;
using BLL.DTO.DriverCategories;
using BLL.DTO.DriverLicensePhotos;
using BLL.DTO.DriverLicenses;
using BLL.DTO.DriverMedicalCertificatePhotos;
using BLL.DTO.DriverMedicalCertificates;
using BLL.DTO.Employees;
using BLL.DTO.Positions;
using BLL.Infrastructure.Dependency;
using BLL.Infrastructure.Mapper.Profiles;
using BLL.Infrastructure.Readers.ReadModels;
using BLL.Interfaces;
using BLL.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ninject;
using System.Globalization;
using Web.Extentions;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmployeeToEmployeeDTOProfile>();
                cfg.AddProfile<PositionToPositionDTOProfile>();
                cfg.AddProfile<DriverCategoryToDriverCategoryDTOProfile>();
                cfg.AddProfile<DriverLicensePhotoToDriverLicensePhotoDTOProfile>();
                cfg.AddProfile<DriverLicenseToDriverLicenseDTOProfile>();
                cfg.AddProfile<DriverMedicalCertificateToDriverMedicalCertificateDTOProfile>();
                cfg.AddProfile<DriverMedicalCertificatePhotoToDriverMedicalCertificatePhotoDTOProfile>();

            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            IKernel ninjectKernel = new StandardKernel(new InterfacesRegistrationsBLL(Configuration.GetConnectionString("LaborProtectionDatabase")));
            IUnitOfWorkService unitOfWorkService = ninjectKernel.Get<IUnitOfWorkService>();

            services.AddScoped<ICRUDDataBaseService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO>>(o => new EmployeeService(unitOfWorkService, mapper));
            services.AddScoped<ICRUDDataBaseService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>>(o => new PositionService(unitOfWorkService, mapper));
            services.AddScoped<ICRUDDataBaseService<DriverCategoryGetUpdateDTO, DriverCategoryAddDTO, DriverCategoryGetUpdateDTO>>(o => new DriverCategoryService(unitOfWorkService, mapper));
            services.AddScoped<ICRUDDataBaseService<DriverLicenseGetDTO, DriverLicenseAddDTO, DriverLicenseUpdateDTO>>(o => new DriverLicenseService(unitOfWorkService, mapper));
            services.AddScoped<ICRUDDataBaseService<DriverMedicalCertificateGetDTO, DriverMedicalCertificateAddDTO, DriverMedicalCertificateUpdateDTO>>(o => new DriverMedicalCertificateService(unitOfWorkService, mapper));
            services.AddScoped<ICRUDDataBaseService<DriverLicensePhotoGetDTO, DriverLicensePhotoAddDTO, DriverLicensePhotoUpdateDTO>>(o => new DriverLicensePhotoService(unitOfWorkService, mapper));
            services.AddScoped<ICRUDDataBaseService<DriverMedicalCertificatePhotoGetDTO, DriverMedicalCertificatePhotoAddDTO, DriverMedicalCertificatePhotoUpdateDTO>>(o => new DriverMedicalCertificatePhotoService(unitOfWorkService, mapper));
            services.AddScoped<IUploadDataFromFileService<XLWorkbook, ReadModelForExcel>>(o => new UploadDataFromExcelService(unitOfWorkService, mapper));

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                })
                .AddViewLocalization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            services.AddSwaggerDocumentation();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwaggerDocumentation();
            app.UseStaticFiles();
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            app.UseErrorHandlingMiddleware();
            app.UseMvc();
        }
    }
}
