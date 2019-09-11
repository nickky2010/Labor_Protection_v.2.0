using BLL.DTO.Identity;
using BLL.Services;
using DAL.EFContexts.Contexts;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountService: IService<LaborProtectionContext>
    {
        Task<IAppActionResult> GetUserAsync(UserDTO userDTO);
        Task<IAppActionResult> GetRoleAsync(string roleName);
        Task<IAppActionResult> AddUserAsync(UserDTO userDTO);
        Task<IAppActionResult> AddRoleAsync(RoleDTO roleDTO);
    }
}
