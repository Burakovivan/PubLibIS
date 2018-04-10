using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubLibIS.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PubLibIS.DAL.Identity
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, int , ApplicationUserRole>
    {
        public ApplicationRoleStore(LibraryEntityFrameworkContext context) : base(context) { }
    }
}
