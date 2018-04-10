using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using PubLibIS.DAL.Identity;
using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Repositories.EntityFramework;

namespace PubLibIS.DAL.UnitsOfWork
{
    public class LibraryUnitOfWorkEntityFramework : IUnitOfWork
    {
        private LibraryEntityFrameworkContext db;
        private bool disposed = false;

        public LibraryUnitOfWorkEntityFramework(string connectionName)
        {
            db = new LibraryEntityFrameworkContext(connectionName);
        }

        private AuthorRepository authorRepository;
        private BookRepository bookRepository;
        private BrochureRepository brochureRepository;
        private BackupFileRepository backupFileRepository;
        private PeriodicalRepository periodicalRepository;
        private PeriodicalEditionRepository periodicalEditionRepository;
        private PublishingHouseRepository publishingHouseRepository;
        private PublishedBookRepository publishedBookRepository;
        private AuthorInBookRepository authorInBookRepository;
        private UserProfileManager userProfileManager;
        private ApplicationRoleManager roleManager;
        private ApplicationUserManager userManager;


        public IAuthorRepository Authors
        {
            get
            {
                if(authorRepository == null)
                {
                    authorRepository = new AuthorRepository(db);
                } return authorRepository;
            }
        }
        public IAuthorInBookRepository AuthorsInBooks
        {
            get
            {
                if(authorInBookRepository == null)
                {
                    authorInBookRepository = new AuthorInBookRepository(db);
                }
                return authorInBookRepository;
            }
        }

        public IBookRepository Books
        {
            get
            {
                if(bookRepository == null)
                {
                    bookRepository = new BookRepository(db);
                }
                return bookRepository;
            }
        }

        public IBrochureRepository Brochures
        {
            get
            {
                if(brochureRepository == null)
                {
                    brochureRepository = new BrochureRepository(db);
                }
                return brochureRepository;
            }
        }

        public IBackupFileRepository BackupFiles
        {
            get
            {
                if(backupFileRepository == null)
                {
                    backupFileRepository = new BackupFileRepository(db);
                }
                return backupFileRepository;
            }
        }

        public IPeriodicalRepository Periodicals
        {
            get
            {
                if(periodicalRepository == null)
                {
                    periodicalRepository = new PeriodicalRepository(db);
                }
                return periodicalRepository;
            }
        }

        public IPeriodicalEditionRepository PeriodicalEditions
        {
            get
            {
                if(periodicalEditionRepository == null)
                {
                    periodicalEditionRepository = new PeriodicalEditionRepository(db);
                }
                return periodicalEditionRepository;
            }
        }

        public IPublishingHouseRepository PublishingHouses
        {
            get
            {
                if(publishingHouseRepository == null)
                {
                    publishingHouseRepository = new PublishingHouseRepository(db);
                }
                return publishingHouseRepository;
            }
        }

        public IPublishedBookRepository PublishedBooks
        {
            get
            {
                if(publishedBookRepository == null)
                {
                    publishedBookRepository = new PublishedBookRepository(db);
                }
                return publishedBookRepository;
            }
        }

        public IUserProfileManager UserProfileManager
        {
            get
            {
                if(userProfileManager == null)
                {
                    userProfileManager = new UserProfileManager(db);
                }
                return userProfileManager;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if(userManager == null)
                {
                    userManager = new ApplicationUserManager(new ApplicationUserStore(db));
                }
                return userManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if(roleManager == null)
                {
                    roleManager = new ApplicationRoleManager(new ApplicationRoleStore(db));
                }
                return roleManager;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task SaveAsync()
        {
           await db.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        public void TrucnateAllTables()
        {
            db.Authors.RemoveRange(db.Authors);
            db.PublishedBooks.RemoveRange(db.PublishedBooks);
            db.Books.RemoveRange(db.Books);
            db.Brochures.RemoveRange(db.Brochures);
            db.Periodicals.RemoveRange(db.Periodicals);
            db.PeriodicalEditions.RemoveRange(db.PeriodicalEditions);
            db.PublishingHouses.RemoveRange(db.PublishingHouses);
            db.PublishedBooks.RemoveRange(db.PublishedBooks);
            db.SaveChanges();
        }

        
    }
}
