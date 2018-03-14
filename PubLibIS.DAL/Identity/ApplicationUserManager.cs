using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PubLibIS.DAL.Models;

namespace PubLibIS.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(UserStore<ApplicationUser> store) : base(store) { }
    }
}
