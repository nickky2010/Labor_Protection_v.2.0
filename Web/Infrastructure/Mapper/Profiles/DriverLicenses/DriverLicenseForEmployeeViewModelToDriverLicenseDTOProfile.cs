using AutoMapper;
using BLL.DTO;
using Web.ViewModels.DriverLicenses;

namespace Web.Infrastructure.Mapper.Profiles.DriverLicenses
{
    public class DriverLicenseForEmployeeViewModelToDriverLicenseDTOProfile : Profile
    {
        public DriverLicenseForEmployeeViewModelToDriverLicenseDTOProfile()
        {
            CreateMap<DriverLicenseDTO, DriverLicenseForEmployeeViewModel>().ReverseMap();
        }
    }
}
