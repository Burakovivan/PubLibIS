using PubLibIS.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using PubLibIS.Domain.Entities;
using PubLibIS.DAL.ResponseModels;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public void Update(Book Book, IEnumerable<AuthorInBook> authorInBookList = null)
        {
            base.Update(Book);

            ResetAuthors(Book.Id, authorInBookList);

        }

        private void ResetAuthors(int bookId, IEnumerable<AuthorInBook> authorInBookList)
        {
            var authorInBookRepo = new AuthorInBookRepository(dapperConnectionFactory);
            authorInBookRepo.GetByBookId(bookId).ToList().ForEach(ainb =>
                authorInBookRepo.Delete(ainb)
            );
            authorInBookList?.ToList().ForEach(ainb =>
            {
                authorInBookRepo.Create(new AuthorInBook { Book_Id = bookId, Author_Id = ainb.Author_Id });
            });
        }
        public int Create(Book b, IEnumerable<AuthorInBook> authorInBookList = null)
        {
            var newId = base.Create(b);
            ResetAuthors(b.Id, authorInBookList);
            return newId;
        }

        public GetBookResponseModel GetBookResponseModel(int bookId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Book> GetBookList()
        {
            return GetList();
        }

        public IEnumerable<Book> GetBookList(IEnumerable<int> idList)
        {
           return GetList(idList);
        }

        public IEnumerable<GetBookResponseModel> GetBookResponseModelList()
        {
            IEnumerable<Book> bookList = GetList();
            return ToResponseModel(bookList);
        }

        public IEnumerable<GetBookResponseModel> GetBookResponseModelList(int skip, int take)
        {
            IEnumerable<Book> bookList = GetList(skip, take);
            return ToResponseModel(bookList);

        }
        private IEnumerable<GetBookResponseModel> ToResponseModel(IEnumerable<Book> source)
        {
            var authorInBookRepository = new AuthorInBookRepository(dapperConnectionFactory);
            var publishedBookRepository = new PublishedBookRepository(dapperConnectionFactory);
            IEnumerable<GetBookResponseModel> response = source.Select(book =>
            {
                return new GetBookResponseModel
                {
                    Id = book.Id,
                    AdditionalData = book.AdditionalData,
                    Capation = book.Capation,
                    ISBN = book.ISBN,
                    Authors = authorInBookRepository.GetAuthorInBookResponseModelByBookId(book.Id),
                    PublishedBooks = publishedBookRepository.GetPublishedBookByBookId(book.Id)
                };
            });
            return response;
        }

        public IEnumerable<GetBookResponseModel> GetBookResponseModelList(IEnumerable<int> idList)
        {
            IEnumerable<Book> bookList = GetList(idList);
            return ToResponseModel(bookList);
        }

        public IEnumerable<Book> GetBookList(int skip, int take)
        {
            return GetList(skip, take);
        }
    }
}
