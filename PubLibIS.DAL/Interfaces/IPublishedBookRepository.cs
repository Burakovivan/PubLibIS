﻿using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.Interfaces
{
    public interface IPublishedBookRepository
    {
        int Create(PublishedBook pBook);
        PublishedBook Get(int pBookId);
        IEnumerable<PublishedBook> Get();
        IEnumerable<PublishedBook> GetPublishedBookByBookId(int bookId);
        IEnumerable<PublishedBook> Get(int skip, int take);
        void Update(PublishedBook pBook);
        void Delete(int pBookId);
    }
}
