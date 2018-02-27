using PubLibIS_DAL.Interfaces;
using PubLibIS_DAL.IoC.MSSQL;
using PubLibIS_DAL.Model;

namespace PubLibIS_DAL.IoC
{
    public class LibraryRepository
    {
        private static LibraryRepository instance;
        public static LibraryRepository GetInstance()
        {
            if (instance == null)
                instance = new LibraryRepository();
            return instance;
        }
        private LibraryRepository()
        {
            var context = LibraryContext.GetInstance();
            RepositoryService = new ArticleRepository(context);
            AuthorRepository = new AuthorRepository(context);
            BookRepository = new BookRepository(context);
            PeriodicalRepository = new PeriodicalRepository(context);
            PeriodicalEditionRepository = new PeriodicalEditionRepository(context);
            PublishingHouseRepository = new PublishingHouseRepository(context);
            PublishedBookRepository = new PublishedBookRepository(context);
        }

        public IArticleRepository RepositoryService { get; private set; }
        public IAuthorRepository AuthorRepository { get; private set; }
        public IBookRepository BookRepository { get; private set; }
        public IPeriodicalRepository PeriodicalRepository { get; private set; }
        public IPeriodicalEditionRepository PeriodicalEditionRepository { get; private set; }
        public IPublishingHouseRepository PublishingHouseRepository { get; private set; }
        public IPublishedBookRepository PublishedBookRepository { get; private set; }
    }
}
