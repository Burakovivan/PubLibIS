using PubLibIS.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Authors { get; }
        IBookRepository Books { get; }
        IBrochureRepository Brochures { get; }
        IBackupFileRepository BackupFiles { get; }
        IPeriodicalRepository Periodicals { get; }
        IPeriodicalEditionRepository PeriodicalEditions { get; }
        IPublishingHouseRepository PublishingHouses { get; }
        IPublishedBookRepository PublishedBooks { get; }
        IAuthorInBookRepository AuthorsInBooks { get; }

        IUserProfileManager UserProfileManager { get; }
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }

        void Save();
        Task SaveAsync();
    }
}
