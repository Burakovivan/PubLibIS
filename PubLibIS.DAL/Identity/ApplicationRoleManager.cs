using Microsoft.AspNet.Identity;
using PubLibIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.Identity
{
    public class ApplicationRoleManager: RoleManager<ApplicationRole, int>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, int> store) : base(store)
        {
        }
    }
}
