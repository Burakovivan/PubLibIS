﻿using PubLibIS.DAL.Interfaces;
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
        private PeriodicalRepository periodicalRepository;
        private PeriodicalEditionRepository periodicalEditionRepository;
        private PublishingHouseRepository publishingHouseRepository;
        private PublishedBookRepository publishedBookRepository;
        private AuthorInBookRepository authorInBookRepository;


        public IAuthorRepository Authors
        {
            get
            {
                if (authorRepository == null)
                    authorRepository = new AuthorRepository(db);
                return authorRepository;
            }
        }
        public IAuthorInBookRepository AuthorsInBooks
        {
            get
            {
                if (authorInBookRepository == null)
                    authorInBookRepository = new AuthorInBookRepository(db);
                return authorInBookRepository;
            }
        }

        public IBookRepository Books
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BookRepository(db);
                return bookRepository;
            }
        }

        public IBrochureRepository Brochures
        {
            get
            {
                if (brochureRepository == null)
                    brochureRepository = new BrochureRepository(db);
                return brochureRepository;
            }
        }
        public IPeriodicalRepository Periodicals
        {
            get
            {
                if (periodicalRepository == null)
                    periodicalRepository = new PeriodicalRepository(db);
                return periodicalRepository;
            }
        }

        public IPeriodicalEditionRepository PeriodicalEditions
        {
            get
            {
                if (periodicalEditionRepository == null)
                    periodicalEditionRepository = new PeriodicalEditionRepository(db);
                return periodicalEditionRepository;
            }
        }

        public IPublishingHouseRepository PublishingHouses
        {
            get
            {
                if (publishingHouseRepository == null)
                    publishingHouseRepository = new PublishingHouseRepository(db);
                return publishingHouseRepository;
            }
        }

        public IPublishedBookRepository PublishedBooks
        {
            get
            {
                if (publishedBookRepository == null)
                    publishedBookRepository = new PublishedBookRepository(db);
                return publishedBookRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
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
    }
}
