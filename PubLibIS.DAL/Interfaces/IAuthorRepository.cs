using PubLibIS.DAL.ResponseModels;
using PubLibIS.Domain.Entities;
using System.Collections.Generic;

namespace PubLibIS.DAL.Interfaces
{
    public interface IAuthorRepository
    {
        GetAuthorResponseModel GetAuthorResponseModel(int authorId);
        Author GetAuthor(int authorId);
        IEnumerable<GetAuthorResponseModel> GetAuthorResponseModelList();
        IEnumerable<GetAuthorResponseModel> GetAuthorResponseModelList(IEnumerable<int> idList);
        IEnumerable<GetAuthorResponseModel> GetAuthorResponseModelList(int skip, int take);
        int Create(Author auhtor);
        void Update(Author author);
        void Delete(int authorId);
    }
}
