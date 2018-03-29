using System.Threading.Tasks;
using DapperExtensions.Mapper;
using Microsoft.AspNet.Identity.EntityFramework;
using PubLibIS.DAL.Identity;
using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using PubLibIS.DAL.Repositories.Dapper;

namespace PubLibIS.DAL.UnitsOfWork
{
    public class LibraryUnitOfWorkDapper : IUnitOfWork
    {
        private LibraryEntityFrameworkContext db;
        private DapperConnectionFactory dapperConnectionFactory;
        private bool disposed = false;

        public LibraryUnitOfWorkDapper(string connectionName)
        {
            db = new LibraryEntityFrameworkContext(connectionName);
            dapperConnectionFactory = new DapperConnectionFactory(connectionName);
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(PluralizedAutoClassMapper<>);
        }

        private AuthorRepository authorRepository;
        private AuthorInBookRepository authorInBookRepository;
        private BookRepository bookRepository;
        private BrochureRepository brochureRepository;
        private PeriodicalRepository periodicalRepository;
        private PeriodicalEditionRepository periodicalEditionRepository;
        private PublishedBookRepository publishedBookRepository;
        private PublishingHouseRepository publishingHouseRepository;
        private Repositories.EntityFramework.UserProfileManager userProfileManager;
        private ApplicationRoleManager roleManager;
        private ApplicationUserManager userManager;


        public IAuthorRepository Authors
        {
            get
            {
                if(authorRepository == null)
                {
                    authorRepository = new AuthorRepository(dapperConnectionFactory);
                }
                return authorRepository;
            }
        }
        public IAuthorInBookRepository AuthorsInBooks
        {
            get
            {
                if(authorInBookRepository == null)
                {
                    authorInBookRepository = new AuthorInBookRepository(dapperConnectionFactory);
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
                    bookRepository = new BookRepository(dapperConnectionFactory);
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
                    brochureRepository = new BrochureRepository(dapperConnectionFactory);
                }
                return brochureRepository;
            }
        }

        public IPeriodicalRepository Periodicals
        {
            get
            {
                if(periodicalRepository == null)
                {
                    periodicalRepository = new PeriodicalRepository(dapperConnectionFactory);
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
                    periodicalEditionRepository = new PeriodicalEditionRepository(dapperConnectionFactory);
                }
                return periodicalEditionRepository;
            }
        }

        public IPublishedBookRepository PublishedBooks
        {
            get
            {
                if(publishedBookRepository == null)
                {
                    publishedBookRepository = new PublishedBookRepository(dapperConnectionFactory);
                }
                return publishedBookRepository;
            }
        }
        public IPublishingHouseRepository PublishingHouses
        {
            get
            {
                if(publishingHouseRepository == null)
                {
                    publishingHouseRepository = new PublishingHouseRepository(dapperConnectionFactory);
                }
                return publishingHouseRepository;
            }
        }


        public IUserProfileManager UserProfileManager
        {
            get
            {
                if(userProfileManager == null)
                {
                    userProfileManager = new Repositories.EntityFramework.UserProfileManager(db);
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
                    userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
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
                    roleManager = new ApplicationRoleManager(new RoleStore<ApplicationUserRole>(db));
                }
                return roleManager;
            }
        }

        public void Save()
        {

        }

        public async Task SaveAsync()
        {

        }

        public virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if(disposing)
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
    }
}
