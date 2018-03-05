using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.Models
{
    class LibraryInitializer: DropCreateDatabaseIfModelChanges<LibraryEntityFrameworkContext>
    {


        protected override void Seed(LibraryEntityFrameworkContext context)
        {
            Author a1 = new Author
            {
                FirstName = "Александр",
                SecondName = "Пушкин",
                Patronymic = "Сергреевич",
                DateOfBirth = new DateTime(1799, 05, 26),
                DateOfDeath = new DateTime(1837, 01, 29)
            };
            Author a2 = new Author
            {
                FirstName = "Тарас",
                SecondName = "Шевченко",
                Patronymic = "Григорьевич",
                DateOfBirth = new DateTime(1814, 03, 9),
                DateOfDeath = new DateTime(1861, 03, 10)
            };

            PublishingHouse ph1 = new PublishingHouse
            {
                Name = "Весна",
                Country = "Украина",
                City = "Харьков",
                Address = "пр. Гагарина, д.20",
                PostalCode = "61010",
                Phone = "+38 (057) 760 23 89",
                FoundationDate = new DateTime(2016, 01, 01)
            };

            context.Authors.Add(a1);
            context.Authors.Add(a2);
            context.PublishingHouses.Add(ph1);

            Book b1 = new Book
            {
                Capation = "Капитанская дочь",
                ISBN = "978-5-386-06612-3",
                AdditionalData = "klasseca",
            };

            context.Books.Add(b1);
            AuthorInBook ainb1 = new AuthorInBook
            {
                Author = a1,
                Book = b1
            };

            PublishedBook pb1 = new PublishedBook()
            {
                Book = b1,
                DateOfPublication = new DateTime(2013, 01, 01),
                PublishingHouse = ph1,
                Volume = 336
            };

            context.AuthorsInBooks.Add(ainb1);
            context.PublishedBooks.Add(pb1);
            context.SaveChanges();


            base.Seed(context);
            var s = context.Authors.ToList();
        }


    }
}
