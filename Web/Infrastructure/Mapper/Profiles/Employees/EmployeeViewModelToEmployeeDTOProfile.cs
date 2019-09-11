using AutoMapper;
using BLL.DTO;
using Web.ViewModels.Employees;

namespace BLL.Infrastructure.Mapper.Profiles
{
    public class EmployeeViewModelToEmployeeDTOProfile : Profile
    {
        public EmployeeViewModelToEmployeeDTOProfile()
        {
            CreateMap<EmployeeDTO, EmployeeViewModel>().ReverseMap();
        }
    }
}
