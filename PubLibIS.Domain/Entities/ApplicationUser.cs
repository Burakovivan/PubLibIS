using Microsoft.AspNet.Identity.EntityFramework;

namespace PubLibIS.Domain.Entities
{
    public class ApplicationUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public virtual UserProfile UserProfile { get; set; }
    }
}
