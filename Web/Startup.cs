using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Infrastructure.Dependency;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ninject;
using Web.Extentions;
using Web.Middleware;
using AutoMapper;
using BLL.Infrastructure.Mapper.Profiles;
using BLL.Infrastructure.Mapper.Profiles.Identity;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using BLL.Services;
using BLL;
using BLL.DTO;
using Web.Infrastructure.Mapper.Profiles.DriverLicenses;
using Web.Infrastructure.Mapper.Profiles.Positions;
using Web.Infrastructure.Mapper.Profiles.DriverMedicalCertificates;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RoleToRoleDTOProfile>();
                cfg.AddProfile<UserProfileToUserProfileDTOProfile>();
                cfg.AddProfile<UserToUserDTOProfile>();
                cfg.AddProfile<DriverLicenseToDriverLicenseDTOProfile>();
                cfg.AddProfile<DriverMedicalCertificateToDriverMedicalCertificateDTOProfile>();
                cfg.AddProfile<EmployeeToEmployeeDTOProfile>();
                cfg.AddProfile<PositionToPositionDTOProfile>();

                cfg.AddProfile<EmployeeViewModelToEmployeeDTOProfile>();
                cfg.AddProfile<DriverLicenseForEmployeeViewModelToDriverLicenseDTOProfile>();
                cfg.AddProfile<PositionForEmployeeViewModelToPositionDTOProfile>();
                cfg.AddProfile<DriverMedicalCertificateForEmployeeViewModelToDriverMedicalCertificateDTOProfile>();
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            IKernel ninjectKernel = new StandardKernel(new InterfacesRegistrationsBLL(Configuration.GetConnectionString("LaborProtectionDatabase")));
            IUnitOfWorkService unitOfWorkService = ninjectKernel.Get<IUnitOfWorkService>();

            services.AddScoped<IDataBaseService<EmployeeDTO>>(o => new EmployeeService(unitOfWorkService, mapper));

            services.AddScoped<IAccountService>(o => new AccountService(unitOfWorkService, mapper));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(cfg =>
                    {
                        cfg.RequireHttpsMetadata = false;
                        cfg.SaveToken = true;
                        cfg.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidIssuer = "me",
                            ValidAudience = "you",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("rlyaKithdrYVl6Z80ODU350md")) //Secret
                        };
                    });

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwaggerDocumentation();
            app.UseStaticFiles();
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            app.UseErrorHandlingMiddleware();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
