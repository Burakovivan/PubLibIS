using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS_DAL.Model
{
    class LibraryInitializer: DropCreateDatabaseIfModelChanges<LibraryContext>
    {


        protected override void Seed(LibraryContext context)
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
            context.SaveChanges();


            base.Seed(context);
        }


    }
}
