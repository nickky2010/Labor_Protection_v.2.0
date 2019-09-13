﻿using BLL.Infrastructure.Dependency;
using BLL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ninject;
using Web.Extentions;
using AutoMapper;
using BLL.Infrastructure.Mapper.Profiles;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using BLL.Services;
using BLL;
using BLL.DTO.Employees;
using BLL.DTO.Positions;

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
                
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            IKernel ninjectKernel = new StandardKernel(new InterfacesRegistrationsBLL(Configuration.GetConnectionString("LaborProtectionDatabase")));
            IUnitOfWorkService unitOfWorkService = ninjectKernel.Get<IUnitOfWorkService>();

            services.AddScoped<IDataBaseService<EmployeeGetDTO, EmployeeAddDTO, EmployeeUpdateDTO>>(o => new EmployeeService(unitOfWorkService, mapper));
            services.AddScoped<IDataBaseService<PositionGetUpdateDTO, PositionAddDTO, PositionGetUpdateDTO>>(o => new PositionService(unitOfWorkService, mapper));

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddDataAnnotationsLocalization(options => {
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
