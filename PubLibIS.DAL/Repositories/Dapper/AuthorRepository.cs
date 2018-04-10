using PubLibIS.DAL.Interfaces;
using System.Linq;
using System.Data;
using PubLibIS.Domain.Entities;
using PubLibIS.DAL.ResponseModels;
using System.Collections.Generic;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }

        public Author GetAuthor(int authorId)
        {
            throw new System.NotImplementedException();
        }

        public GetAuthorResponseModel GetAuthorResponseModel(int id)
        {
            var authorInBookRepo = new AuthorInBookRepository(dapperConnectionFactory);

            Author author = base.Get(id);
            IEnumerable<AuthorInBook> ainbList = authorInBookRepo.GetByAuthorId(id);
            GetAuthorResponseModel response = new GetAuthorResponseModel
            {
                DateOfBirth = author.DateOfBirth,
                DateOfDeath = author.DateOfDeath,
                FirstName = author.FirstName,
                Id = author.Id,
                Patronymic = author.Patronymic,
                SecondName = author.SecondName,
                Books = authorInBookRepo.GetAuthorInBookResponseModelByAuthorId(author.Id)
            };
            return response;
        }


        IEnumerable<GetAuthorResponseModel> IAuthorRepository.GetAuthorResponseModelList()
        {
            IEnumerable<Author> authorList = GetList();
            return ToResponseModel(authorList);
        }

        private IEnumerable<GetAuthorResponseModel> ToResponseModel(IEnumerable<Author> source)
        {
            var authorInBookRepository = new AuthorInBookRepository(dapperConnectionFactory);
            IEnumerable<GetAuthorResponseModel> response = source.Select(author =>
            {
                return new GetAuthorResponseModel
                {
                    Id = author.Id,
                   DateOfBirth = author.DateOfBirth,
                   DateOfDeath = author.DateOfDeath,
                    FirstName = author.FirstName,
                     Patronymic = author.Patronymic,
                      SecondName = author.SecondName,
                      Books = authorInBookRepository.GetAuthorInBookResponseModelByAuthorId(author.Id)
                };
            });
            return response;
        }

        IEnumerable<GetAuthorResponseModel> IAuthorRepository.GetAuthorResponseModelList(IEnumerable<int> idList)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<GetAuthorResponseModel> IAuthorRepository.GetAuthorResponseModelList(int skip, int take)
        {
            throw new System.NotImplementedException();
        }
    }
}
