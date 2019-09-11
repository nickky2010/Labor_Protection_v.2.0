using AutoMapper;
using BLL.DTO.Identity;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.EFContexts.Contexts;
using DAL.Interfaces;
using DAL.Models.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService : Service, IAccountService
    {
        public IUnitOfWork<LaborProtectionContext> UnitOfWork { get; set; }
        public IMapper Mapper { get; set; }
        public IStringLocalizer<SharedResource> Localizer { get; set; }

        public AccountService(IUnitOfWorkService unitOfWorkService, IMapper mapper)
        {
            UnitOfWork = unitOfWorkService.UnitOfWorkLaborProtectionContext;
            Mapper = mapper;
        }
        public async Task<IAppActionResult> GetUserAsync(UserDTO userDTO)
        {
            var user = await UnitOfWork.Users.FindAsync(u => u.UserName == userDTO.UserName && u.Email == userDTO.Email);
            if (user != null)
                return new AppActionResult<UserDTO>
                {
                    Data = Mapper.Map<User, UserDTO>(user),
                    Status = (int)HttpStatusCode.OK
                };
            else
                return new AppActionResult { Status = (int)HttpStatusCode.NotFound, ErrorMessages = new List<string> { Localizer["InvalidLoginAndOrPassword"] } };
        }

        public async Task<IAppActionResult> GetRoleAsync(string roleName)
        {
            var role = await UnitOfWork.Roles.FindAsync(r => r.Name == roleName);
            if (role != null)
                return new AppActionResult<RoleDTO>
                {
                    Data = Mapper.Map<Role, RoleDTO>(role),
                    Status = (int)HttpStatusCode.OK
                };
            else
                return new AppActionResult { Status = (int)HttpStatusCode.NotFound, ErrorMessages = new List<string> { Localizer["InvalidRoleName"] } };
        }

        public async Task<IAppActionResult> AddUserAsync(UserDTO userDTO)
        {
            User user = Mapper.Map<UserDTO, User>(userDTO);
            Role role = await UnitOfWork.Roles.FindAsync(r => r.Name == userDTO.Role.Name);
            if (role != null)
            {
                user.RoleId = role.Id;
                user.Role = null;
            }
            else
                return new AppActionResult { Status = (int)HttpStatusCode.NotFound, ErrorMessages = new List<string> { Localizer["RoleNotFound"] } };
            var userCheck = await UnitOfWork.Users.FindAsync(u => u.UserName == userDTO.UserName);
            if (userCheck == null)
            {
                UnitOfWork.Users.AddAsync(user);
                await UnitOfWork.SaveChangesAsync();
                var addedUser = await UnitOfWork.Users.FindAsync(u => u.UserName == userDTO.UserName);
                if (addedUser != null)
                {
                    return new AppActionResult<UserDTO>
                    {
                        Data = Mapper.Map<User, UserDTO>(addedUser),
                        Status = (int)HttpStatusCode.Created
                    };
                }
                else
                    return new AppActionResult { Status = (int)HttpStatusCode.InternalServerError, ErrorMessages = new List<string> { Localizer["AfterAddUserNotFound"] } };
            }
            else
                return new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["LoginAlreadyExists"] } };
        }
        public async Task<IAppActionResult> AddRoleAsync(RoleDTO roleDTO)
        {
            Role role = await UnitOfWork.Roles.FindAsync(r => r.Name == roleDTO.Name);
            if (role != null)
                return new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["RoleAlreadyExists"] } };
            else
            {
                UnitOfWork.Roles.AddAsync(Mapper.Map<RoleDTO, Role>(roleDTO));
                await UnitOfWork.SaveChangesAsync();
            }
            role = await UnitOfWork.Roles.FindAsync(r => r.Name == roleDTO.Name);
            if (role != null)
                return new AppActionResult<RoleDTO>
                {
                    Data = Mapper.Map<Role, RoleDTO>(role),
                    Status = (int)HttpStatusCode.Created
                };
            else
                return new AppActionResult { Status = (int)HttpStatusCode.InternalServerError, ErrorMessages = new List<string> { Localizer["AfterAddRoleNotFound"] } };
        }
    }
}
