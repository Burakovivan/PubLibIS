using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public new void Update(Book Book)
        {
            base.Update(Book);
            ResetAuthors(Book);
        }

        public override void LoadNavigationProperties(Book b, IDbConnection db)
        {
            if(b.PublishedBooks == null)
            {
                var publishedBookRepository = new PublishedBookRepository(dapperConnectionFactory);
                publishedBookRepository.SuperReference = typeof(Book);
                b.PublishedBooks = publishedBookRepository.GetPublishedBookByBookId(b.Id).ToList();
            }

            if(b.Authors == null)
            {
                var authorInBookRepository = new AuthorInBookRepository(dapperConnectionFactory);
                authorInBookRepository.SuperReference = typeof(Book);
                b.Authors = authorInBookRepository.GetByBookId(b.Id).ToList();
            }
        }



        private void ResetAuthors(Book b)
        {
            var authorInBookRepo = new AuthorInBookRepository(dapperConnectionFactory);
            authorInBookRepo.GetByBookId(b.Id).ToList().ForEach(ainb =>
                authorInBookRepo.Delete(ainb)
            );
            b.Authors.ToList().ForEach(ainb =>
            {
                authorInBookRepo.Create(new AuthorInBook { Book_Id = b.Id, Author_Id = ainb.Author_Id });
            });
        }
    }
}
