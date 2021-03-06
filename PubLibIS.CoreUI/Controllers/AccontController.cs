using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PubLibIS.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using PubLibIS.BLL.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PubLibIS.CoreUI.Controllers
{
  [Authorize]
  public class AccountController : Controller
  {
    private UserService userService;
    private IConfiguration configuration;
    public AccountController(UserService UserService, IConfiguration configuration)
    {
      this.configuration = configuration;
      this.userService = UserService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("api/signin")]
    public async Task<IActionResult> SignIn([FromBody]LoginModel loginModel)
    {
      if(!ModelState.IsValid)
      {
        return BadRequest();
      }
      //This method returns user id from username and password.
      ClaimsIdentity claimsIdentity = userService.Authenticate(loginModel).GetAwaiter().GetResult();
      if(claimsIdentity == null)
      {
        return BadRequest(new { message = "Wrong login or password" });
      }
      var userId = claimsIdentity.GetUserId<string>();
      if(string.IsNullOrEmpty(userId))
      {
        return Unauthorized();
      }

      List<Claim> claims = claimsIdentity.Claims.Select(c => new Claim(c.Type, c.Value)).ToList();
      claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


      var token = new JwtSecurityToken
      (
          issuer: configuration["TokenAuthentication:Issuer"],
          audience: configuration["TokenAuthentication:Audience"],
          claims: claims,
          expires: DateTime.UtcNow.AddDays(60),
          notBefore: DateTime.UtcNow,
          signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenAuthentication:SecretKey"])),
                  SecurityAlgorithms.HmacSha256)
      );
      return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

    }
    [AllowAnonymous]
    [HttpPost]
    [Route("api/signup")]
    public async Task<IActionResult> SignUp([FromBody]RegisterModel registerModel)
    {
      if(!ModelState.IsValid)
      {
        return BadRequest(new { message = "Not valid login or password" });
      }
      await userService.Create(registerModel);
      return await SignIn(new LoginModel { Email = registerModel.Email, Password = registerModel.Password });
    }
  }
}
