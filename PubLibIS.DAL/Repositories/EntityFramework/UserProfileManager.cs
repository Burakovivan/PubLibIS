using PubLibIS.DAL.Interfaces;
using PubLibIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.Repositories.EntityFramework
{
    public class UserProfileManager : IUserProfileManager
    {
        public LibraryEntityFrameworkContext Database { get; set; }
        public UserProfileManager(LibraryEntityFrameworkContext db)
        {
            Database = db;
        }

        public void Create(UserProfile item)
        {
            Database.UserProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
