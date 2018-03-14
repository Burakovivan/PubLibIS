using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationUserRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationUserRole> store) : base(store)
        {
        }
    }
}
