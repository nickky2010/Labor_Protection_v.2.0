using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace BLL.Infrastructure.Mapper.Profiles
{
    public class DriverLicenseToDriverLicenseDTOProfile : Profile
    {
        public DriverLicenseToDriverLicenseDTOProfile()
        {
            CreateMap<DriverLicense, DriverLicenseDTO>().ReverseMap();
        }
    }
}
