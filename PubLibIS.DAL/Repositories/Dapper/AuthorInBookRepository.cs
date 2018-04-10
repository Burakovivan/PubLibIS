using PubLibIS.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System.Linq.Expressions;
using PubLibIS.Domain.Entities;
using PubLibIS.DAL.ResponseModels;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class AuthorInBookRepository : Repository<AuthorInBook>, IAuthorInBookRepository
    {

        public AuthorInBookRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public IEnumerable<GetAuthorInBookResponseModel> GetAuthorInBookResponseModelByAuthorId(int authorId)
        {
            IEnumerable<AuthorInBook> authorlList = GetByAuthorId(authorId);
            return ToResponseModel(authorlList);
        }

        public IEnumerable<GetAuthorInBookResponseModel> GetAuthorInBookResponseModelByBookId(int authorId)
        {
            IEnumerable<AuthorInBook> authorlList = GetByBookId(authorId);
            return ToResponseModel(authorlList);
        }

        public IEnumerable<AuthorInBook> GetByAuthorId(int authorId)
        {
            return GetList().Where(ainb => ainb.Author_Id == authorId);
        }

        public IEnumerable<AuthorInBook> GetByAuthorIdList(IEnumerable<int> idList)
        {
            return GetList().Where(ainb => idList.Contains(ainb.Author_Id));
        }

        public IEnumerable<AuthorInBook> GetByBookId(int bookId)
        {
            return GetList().Where(ainb => ainb.Book_Id == bookId);
        }

        public IEnumerable<AuthorInBook> GetByBookIdList(IEnumerable<int> idList)
        {
            return GetList().Where(ainb => idList.Contains(ainb.Book_Id));
        }

        public IEnumerable<GetAuthorInBookResponseModel> ToResponseModel(IEnumerable<AuthorInBook> source)
        {
            var authorRepository = new AuthorRepository(dapperConnectionFactory);
            var bookRepository = new BookRepository(dapperConnectionFactory);
            IEnumerable<GetAuthorInBookResponseModel> response = source.Select(ainb =>
            {
                return new GetAuthorInBookResponseModel
                {
                    Id = ainb.Id,
                    Author = authorRepository.Get(ainb.Author_Id),
                    Book = bookRepository.Get(ainb.Book_Id)
                };
            });
            return response;
        }


    }
}
