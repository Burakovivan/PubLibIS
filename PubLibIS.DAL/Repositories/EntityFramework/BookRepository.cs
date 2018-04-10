using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.ResponseModels;
using PubLibIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PubLibIS.DAL.Repositories.EntityFramework
{
    public class BookRepository : IBookRepository
    {
        private LibraryEntityFrameworkContext context;

        public BookRepository(LibraryEntityFrameworkContext context)
        {
            this.context = context;
        }

        public int Create(Book book, IEnumerable<AuthorInBook> authorInBookList = null)
        {
            context.Books.Add(book);

            if(authorInBookList != null && authorInBookList.Count() > 0)
            {
                IEnumerable<int> bookAuthors = authorInBookList.Select(a => a.Id);
                IQueryable<Author> authors = context.Authors.Where(x => bookAuthors.Contains(x.Id));

                foreach(Author author in authors)
                {
                    context.AuthorsInBooks.Add(new AuthorInBook
                    {
                        Author = author,
                        Book = book
                    });
                }

            }

            context.SaveChanges();
            return book.Id;
        }

        public void Delete(int bookId)
        {
            var book = Get(bookId);
            context.Books.Remove(book);
        }

        public Book Get(int bookId)
        {
            return context.Books.Find(bookId);
        }


        public IEnumerable<Book> GetList()
        {
            return context.Books.ToList();
        }

        public IEnumerable<Book> GetList(int skip, int take)
        {
            return context.Books.OrderBy(book => book.Id).Skip(skip).Take(take).ToList();
        }

        public void Update(Book book, IEnumerable<AuthorInBook> authorInBookList = null)
        {
            ResetAuthros(book.Id);
            var current = context.Books.Find(book.Id);
            var bookAuthors = authorInBookList.Select(a => a.Id);
            var authors = context.Authors.Where(x => bookAuthors.Contains(x.Id));
            foreach(var author in authors)
            {
                context.AuthorsInBooks.Add(new AuthorInBook
                {
                    Author = author,
                    Book = current
                });
            }
            //context.Entry(current).State = System.Data.Entity.EntityState.Modified;
            context.Entry(current).CurrentValues.SetValues(book);
        }
        private void ResetAuthros(int bookId,IEnumerable<AuthorInBook> authorInBookList = null )
        {
            var authorInBookRepo = new AuthorInBookRepository(context);
            authorInBookRepo.GetByBookId(bookId).ToList().ForEach(ainb =>
                authorInBookRepo.Delete(ainb.Id)
            );
            authorInBookList?.ToList().ForEach(ainb =>
            {
                authorInBookRepo.Create(new AuthorInBook { Book_Id = bookId, Author_Id = ainb.Author_Id });
            });
        }

        public IEnumerable<T> Select<T>(Func<Book, T> selector)
        {
            return context.Books.Select(selector).ToList();
        }

        public IEnumerable<T> SelectMany<T>(Func<Book, IEnumerable<T>> selector)
        {
            return context.Books.SelectMany(selector).ToList();
        }

        public IEnumerable<Book> GetPublishingHouseList(IEnumerable<int> idList)
        {
            return context.Books.Where(a => idList.Contains(a.Id)).ToList();
        }

        public int Count()
        {
            return context.Books.Count();
        }

        public GetBookResponseModel GetBookResponseModel(int bookId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBookList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBookList(IEnumerable<int> idList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetBookResponseModel> GetBookResponseModelList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetBookResponseModel> GetBookResponseModelList(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetBookResponseModel> GetBookResponseModelList(IEnumerable<int> idList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBookList(int skip, int take)
        {
            throw new NotImplementedException();
        }
    }
}
