using System;
using System.Security.Claims;
using System.Threading.Tasks;
using PubLibIS.ViewModels;

namespace PubLibIS.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task Create(RegisterModel userProfile);
        Task<ClaimsIdentity> Authenticate(LoginModel userProfile);
    }
}
