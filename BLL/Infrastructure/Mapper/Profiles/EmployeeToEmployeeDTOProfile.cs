using AutoMapper;
using BLL.DTO.Employees;
using DAL.Models;
using System;

namespace BLL.Infrastructure.Mapper.Profiles
{
    internal class EmployeeToEmployeeDTOProfile : Profile
    {
        public EmployeeToEmployeeDTOProfile()
        {
            CreateMap<Employee, EmployeeGetDTO>().ReverseMap();
            CreateMap<Employee, EmployeeAddDTO>()
                .ForPath(d => d.PositionId, opt => opt.MapFrom(s => s.Position.Id));
            CreateMap<EmployeeAddDTO, Employee>()
                .ForPath(d => d.PositionId, opt => opt.MapFrom(s => s.PositionId))
                .AfterMap((s, d) =>
                {
                    d.Id = Guid.NewGuid();
                });

            CreateMap<Employee, EmployeeUpdateDTO>()
                .ForPath(d => d.PositionId, opt => opt.MapFrom(s => s.Position.Id));
            CreateMap<EmployeeUpdateDTO, Employee>()
                .ForPath(d => d.PositionId, opt => opt.MapFrom(s => s.PositionId));

            CreateMap<Employee, EmployeeForModelsDTO>().ReverseMap();
        }
    }
}
