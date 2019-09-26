using AutoMapper;
using BLL.DTO.DriverCategories;
using BLL.DTO.DriverMedicalCertificates;
using DAL.Models;
using DAL.Models.ManyToMany;
using System;

namespace BLL.Infrastructure.Mapper.Profiles
{
    internal class DriverMedicalCertificateToDriverMedicalCertificateDTOProfile : Profile
    {
        public DriverMedicalCertificateToDriverMedicalCertificateDTOProfile()
        {
            CreateMap<DriverMedicalCertificate, DriverMedicalCertificateAddDTO>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverMedicalCertificateDriverCategories)
                        d.DriverCategoriesId.Add(src.DriverCategoryId);
                });
            CreateMap<DriverMedicalCertificateAddDTO, DriverMedicalCertificate>()
                .AfterMap((s, d) => 
                {
                    d.Id = Guid.NewGuid();
                })
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverCategoriesId)
                        d.DriverMedicalCertificateDriverCategories.Add(new DriverMedicalCertificateDriverCategory
                        {
                            DriverCategoryId = src,
                            DriverMedicalCertificateId = d.Id
                        });
                });

            CreateMap<DriverMedicalCertificate, DriverMedicalCertificateUpdateDTO>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverMedicalCertificateDriverCategories)
                        d.DriverCategoriesId.Add(src.DriverCategoryId);
                });
            CreateMap<DriverMedicalCertificateUpdateDTO, DriverMedicalCertificate>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverCategoriesId)
                        d.DriverMedicalCertificateDriverCategories.Add(new DriverMedicalCertificateDriverCategory
                        {
                            DriverCategoryId = src,
                            DriverMedicalCertificateId = d.Id
                        });
                });

            CreateMap<DriverMedicalCertificate, DriverMedicalCertificateGetDTO>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverMedicalCertificateDriverCategories)
                        d.DriverCategories.Add(new DriverCategoryGetUpdateDTO
                        {
                            Id = src.DriverCategory.Id,
                            Name = src.DriverCategory.Name
                        });
                });
            CreateMap<DriverMedicalCertificateGetDTO, DriverMedicalCertificate>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverCategories)
                        d.DriverMedicalCertificateDriverCategories.Add(new DriverMedicalCertificateDriverCategory
                        {
                            DriverCategoryId = src.Id,
                            DriverMedicalCertificateId = d.Id
                        });
                });


            CreateMap<DriverMedicalCertificate, DriverMedicalCertificateGetForEmployeeDTO>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverMedicalCertificateDriverCategories)
                        d.DriverCategories.Add(new DriverCategoryGetUpdateDTO
                        {
                            Id = src.DriverCategory.Id,
                            Name = src.DriverCategory.Name
                        });
                });
            CreateMap<DriverMedicalCertificateGetForEmployeeDTO, DriverMedicalCertificate>()
                .AfterMap((s, d) =>
                {
                    foreach (var src in s.DriverCategories)
                        d.DriverMedicalCertificateDriverCategories.Add(new DriverMedicalCertificateDriverCategory
                        {
                            DriverCategoryId = src.Id,
                            DriverMedicalCertificateId = d.Id
                        });
                });
        }
    }
}
