using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PubLibIS.DAL.Models;

namespace PubLibIS.BLL.JsonModels
{
    public class AuthorJsonAggregator
    {
        public IEnumerable<Author> Authors;
    }
}
