using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System.Linq.Expressions;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class AuthorInBookRepository : Repository<AuthorInBook>, IAuthorInBookRepository
    {

        public AuthorInBookRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

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

        public override void LoadNavigationProperties(AuthorInBook entity, IDbConnection connection)
        {
            if(entity.Book == null && SuperReference!=typeof(Book))
            {
                var repo = new BookRepository(dapperConnectionFactory);
                entity.Book = repo.Get(entity.Book_Id);

            }
            if(entity.Author == null && SuperReference != typeof(Author))
            {
                var repo = new AuthorRepository(dapperConnectionFactory);
                repo.SuperReference = typeof(AuthorInBookRepository);
                entity.Author = repo.Get(entity.Author_Id);
            }
        }


    }
}
