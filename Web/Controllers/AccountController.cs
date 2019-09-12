using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL;
using BLL.DTO.Identity;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Web.Infrastructure;
using Web.Interfaces;
using Web.ViewModels.Identity;

namespace Web.Controllers
{
    //[Route("api/[controller]")]
    //public class AccountController : ControllerExt<UserDTO, LoginViewModel>, IControllerServices<AccountController, IAccountService>
    //{
    //    public IStringLocalizer<SharedResource> Localizer { get; private set; }
    //    public IMapper Mapper { get; private set; }
    //    public IAccountService Service { get; private set; }

    //    public AccountController(IAccountService service, IStringLocalizer<SharedResource> localizer, IMapper mapper)
    //    {
    //        Service = service;
    //        Localizer = localizer;
    //        Service.Localizer = Localizer;
    //        Mapper = mapper;
    //    }

    //    [AllowAnonymous]
    //    [Route("api/token")]
    //    [HttpPost]
    //    public async Task<IAppActionResult> Token([FromBody]UserDTO userDTO)
    //    {
    //        //if (!ModelState.IsValid)
    //        //    return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["DataIsNotValid"] } });
    //        //var userIdentifiedResult = await Service.GetUserAsync(userDTO);
    //        //if (userIdentifiedResult == null)
    //        //    return SetResult(new AppActionResult { Status = (int)HttpStatusCode.Unauthorized, ErrorMessages = new List<string> { Localizer["InvalidLoginAndOrPassword"] } });
    //        //if (userIdentifiedResult is AppActionResult<UserDTO>)
    //        //{
    //        //    var claims = new[]
    //        //    {
    //        //        new Claim(JwtRegisteredClaimNames.UniqueName, "data"),
    //        //        new Claim(JwtRegisteredClaimNames.Sub, "data"),
    //        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //        //        new Claim(ClaimsIdentity.DefaultNameClaimType, ((AppActionResult<UserDTO>)userIdentifiedResult).Data.UserName),
    //        //        new Claim(ClaimsIdentity.DefaultRoleClaimType, ((AppActionResult<UserDTO>)userIdentifiedResult).Data.Role.Name)
    //        //    };
    //        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("rlyaKithdrYVl6Z80ODU350md")); //Secret
    //        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    //        //    var token = new JwtSecurityToken("me",
    //        //        "you",
    //        //        claims,
    //        //        expires: DateTime.Now.AddMinutes(5),
    //        //        signingCredentials: creds);
    //        //    return SetResult(new AppActionResultToken
    //        //    {
    //        //        Status = (int)HttpStatusCode.Accepted,
    //        //        AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
    //        //        ExpiresIn = DateTime.Now.AddMinutes(5),
    //        //        TokenType = "bearer"
    //        //    });
    //        //}
    //        //return SetResult(userIdentifiedResult);
    //    }

    //    // POST api/<controller>
    //    [AllowAnonymous]
    //    [Route("api/postUser")]
    //    [HttpPost]
    //    public async Task<IAppActionResult> PostUser([FromBody]UserDTO userDTO)
    //    {
    //        if (userDTO == null)
    //            return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["NoData"] } });
    //        if (userDTO.Role.Name == "admin")
    //            return SetResult(new AppActionResult { Status = (int)HttpStatusCode.Forbidden, ErrorMessages = new List<string> { Localizer["СreatingWithRoleIsProhibited"] } });
    //        if (!ModelState.IsValid)
    //            return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["DataIsNotValid"] } });
    //        return SetResult(await Service.AddUserAsync(userDTO));
    //    }

    //    [Authorize(Roles = "admin")]
    //    [Route("api/postRole")]
    //    [HttpPost]
    //    public async Task<IAppActionResult> PostRole([FromBody]RoleDTO roleDTO)
    //    {
    //        if (roleDTO == null)
    //            return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["NoData"] } });
    //        if (!ModelState.IsValid)
    //            return SetResult(new AppActionResult { Status = (int)HttpStatusCode.BadRequest, ErrorMessages = new List<string> { Localizer["DataIsNotValid"] } });
    //        return SetResult(await Service.AddRoleAsync(roleDTO));
    //    }
    //}
}
