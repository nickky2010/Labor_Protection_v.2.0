using AutoMapper;
using BLL.DTO.DriverCategories;
using BLL.DTO.DriverLicenses;
using DAL.Models;
using DAL.Models.ManyToMany;
using System;

namespace BLL.Infrastructure.Mapper.Profiles
{
    internal class DriverLicenseToDriverLicenseDTOProfile : Profile
    {
        public DriverLicenseToDriverLicenseDTOProfile()
        {
            // Add
            CreateMap<DriverLicense, DriverLicenseAddDTO>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverLicenseDriverCategories)
                        d.DriverCategoriesId.Add(src.DriverCategoryId);
                });
            CreateMap<DriverLicenseAddDTO, DriverLicense>()
                .AfterMap((s, d) => 
                {
                    d.Id = Guid.NewGuid();
                })
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverCategoriesId)
                        d.DriverLicenseDriverCategories.Add(new DriverLicenseDriverCategory
                        {
                            DriverCategoryId = src,
                            DriverLicenseId = d.Id
                        });
                });

            // Update
            CreateMap<DriverLicense, DriverLicenseUpdateDTO>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverLicenseDriverCategories)
                        d.DriverCategoriesId.Add(src.DriverCategoryId);
                });
            CreateMap<DriverLicenseUpdateDTO, DriverLicense>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverCategoriesId)
                        d.DriverLicenseDriverCategories.Add(new DriverLicenseDriverCategory
                        {
                            DriverCategoryId = src,
                            DriverLicenseId = d.Id
                        });
                });

            // Get
            CreateMap<DriverLicense, DriverLicenseGetDTO>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverLicenseDriverCategories)
                        d.DriverCategories.Add(new DriverCategoryGetUpdateDTO
                        {
                            Id = src.DriverCategory.Id,
                            Name = src.DriverCategory.Name
                        });
                });
            CreateMap<DriverLicenseGetDTO, DriverLicense>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverCategories)
                        d.DriverLicenseDriverCategories.Add(new DriverLicenseDriverCategory
                        {
                            DriverCategoryId = src.Id,
                            DriverLicenseId = d.Id
                        });
                });


            CreateMap<DriverLicense, DriverLicenseGetForEmployeeDTO>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverLicenseDriverCategories)
                        d.DriverCategories.Add(new DriverCategoryGetUpdateDTO
                        {
                            Id = src.DriverCategory.Id,
                            Name = src.DriverCategory.Name
                        });
                });
            CreateMap<DriverLicenseGetForEmployeeDTO, DriverLicense>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverCategories)
                        d.DriverLicenseDriverCategories.Add(new DriverLicenseDriverCategory
                        {
                            DriverCategoryId = src.Id,
                            DriverLicenseId = d.Id
                        });
                });
        }
    }
}
