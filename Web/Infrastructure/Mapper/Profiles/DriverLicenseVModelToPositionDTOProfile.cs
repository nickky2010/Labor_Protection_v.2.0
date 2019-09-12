using AutoMapper;
using BLL.DTO;
using Web.ViewModels.DriverLicenses;

namespace Web.Infrastructure.Mapper.Profiles
{
    public class DriverLicenseVModelToDriverLicenseDTOProfile : Profile
    {
        public DriverLicenseVModelToDriverLicenseDTOProfile()
        {
            CreateMap<DriverLicenseDTO, DriverLicenseAddVModel>().ReverseMap();
            CreateMap<DriverLicenseDTO, DriverLicenseGetVModel>().ReverseMap();
            CreateMap<DriverLicenseDTO, DriverLicenseUpdateVModel>().ReverseMap();
            CreateMap<DriverLicenseDTO, DriverLicenseForCategoryVModel>().ReverseMap();
            CreateMap<DriverLicenseDTO, DriverLicenseForEmployeeVModel>().ReverseMap();
        }
    }
}
