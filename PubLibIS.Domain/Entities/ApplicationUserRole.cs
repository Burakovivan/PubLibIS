using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace PubLibIS.Domain.Entities
{
    public class ApplicationUserRole :  IdentityUserRole<int>, IDisposable
    {
        public void Dispose()
        {
        }
    }
}
