using PubLibIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.ResponseModels
{
    public class GetPublishedBookResponseModel
    {
    public int Id { get; set; }
    public DateTime? DateOfPublication { get; set; }
    public int? PublishingHouse_Id { get; set; }
    public int? Book_Id { get; set; }
    public PublishingHouse PublishingHouse { get; set; }
    public Book Book { get; set; }
    }
}
