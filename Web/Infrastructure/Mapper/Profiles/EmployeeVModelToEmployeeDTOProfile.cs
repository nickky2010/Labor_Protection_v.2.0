using AutoMapper;
using BLL.DTO;
using System.Collections.Generic;
using Web.ViewModels.Employees;

namespace BLL.Infrastructure.Mapper.Profiles
{
    public class EmployeeVModelToEmployeeDTOProfile : Profile
    {
        public EmployeeVModelToEmployeeDTOProfile()
        {
            CreateMap<EmployeeDTO, EmployeeAddVModel>()
                .ForPath(d => d.Position, opt => opt.MapFrom(s => s.Position.Name));
            CreateMap<EmployeeAddVModel, EmployeeDTO>()
                .ForPath(d => d.Position.Name, opt => opt.MapFrom(s => s.Position));

            CreateMap<EmployeeDTO, EmployeeGetVModel>()
                .ForPath(d => d.Position, opt => opt.MapFrom(s => s.Position.Name));
            CreateMap<EmployeeGetVModel, EmployeeDTO>()
                .ForPath(d => d.Position.Name, opt => opt.MapFrom(s => s.Position));

            CreateMap<EmployeeDTO, EmployeeUpdateVModel>()
                .ForPath(d => d.Position, opt => opt.MapFrom(s => s.Position.Name));
            CreateMap<EmployeeUpdateVModel, EmployeeDTO>()
                .ForPath(d => d.Position.Name, opt => opt.MapFrom(s => s.Position));

            CreateMap<EmployeeDTO, EmployeeForModelsVModel>().ReverseMap();
        }
    }
}
