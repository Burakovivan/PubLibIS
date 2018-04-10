using Dapper.Contrib.Extensions;
using PubLibIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.ResponseModels
{
    public class GetBookResponseModel
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Capation { get; set; }
        public string AdditionalData { get; set; }

        public IEnumerable<PublishedBook> PublishedBooks { get; set; }
        public IEnumerable<GetAuthorInBookResponseModel> Authors { get; set; }
    }
}
