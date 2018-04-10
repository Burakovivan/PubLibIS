using Microsoft.AspNet.Identity;
using PubLibIS.DAL.Interfaces;
using PubLibIS.Domain.Entities;
using PubLibIS.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PubLibIS.BLL.Services
{
    public class UserService: IDisposable
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public UserService()
        {
        }

        public async Task Create(RegisterModel registerModel)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(registerModel.Email);
            if(user != null)
            {
                throw new ArgumentException("User already created", nameof(registerModel.Email));
            }

            user = new ApplicationUser { Email = registerModel.Email, UserName = registerModel.Email };
            var result = await Database.UserManager.CreateAsync(user, registerModel.Password);
            if(result.Errors.Count() > 0)
                throw new ArgumentException($"User wasn't created:{string.Join(Environment.NewLine, result.Errors.Select(e => "\n" + e))}", nameof(registerModel.Email));
            var role = registerModel.Admin ? "admin" : "user";
            await Database.UserManager.AddToRoleAsync(user.Id, role);

            UserProfile userProfile = new UserProfile { Id = user.Id, Address = registerModel.Address, Name = registerModel.Name };
            Database.UserProfileManager.Create(userProfile);



        }

        public async Task<ClaimsIdentity> Authenticate(LoginModel userProfile)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userProfile.Email, userProfile.Password);
            if(user != null)
            {
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
