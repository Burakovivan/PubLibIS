using PubLibIS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL
{
    class LibraryInitializer : DropCreateDatabaseIfModelChanges<LibraryEntityFrameworkContext>
    {


        protected override void Seed(LibraryEntityFrameworkContext context)
        {
            context.Database.ExecuteSqlCommand("ALTER TABLE Brochures DROP CONSTRAINT [FK_dbo.Brochures_dbo.PublishingHouses_PublishingHouse_Id]");
            context.Database.ExecuteSqlCommand("ALTER TABLE Brochures ADD CONSTRAINT [FK_dbo.Brochures_dbo.PublishingHouses_PublishingHouse_Id] FOREIGN KEY ([PublishingHouse_Id]) REFERENCES [dbo].[PublishingHouses] ([Id]) ON DELETE SET NULL");

            context.Database.ExecuteSqlCommand("ALTER TABLE Periodicals DROP CONSTRAINT [FK_dbo.Periodicals_dbo.PublishingHouses_PublishingHouse_Id]");
            context.Database.ExecuteSqlCommand("ALTER TABLE Periodicals ADD CONSTRAINT [FK_dbo.Periodicals_dbo.PublishingHouses_PublishingHouse_Id] FOREIGN KEY ([PublishingHouse_Id]) REFERENCES [dbo].[PublishingHouses] ([Id]) ON DELETE SET NULL");

            context.Database.ExecuteSqlCommand("ALTER TABLE PublishedBooks DROP CONSTRAINT [FK_dbo.PublishedBooks_dbo.PublishingHouses_PublishingHouse_Id]");
            context.Database.ExecuteSqlCommand("ALTER TABLE PublishedBooks ADD CONSTRAINT [FK_dbo.PublishedBooks_dbo.PublishingHouses_PublishingHouse_Id] FOREIGN KEY ([PublishingHouse_Id]) REFERENCES [dbo].[PublishingHouses] ([Id]) ON DELETE SET NULL");

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
            Author a3 = new Author
            {
                FirstName = "Иван",
                SecondName = "Даль",
                Patronymic = "Владимирович",
                DateOfBirth = new DateTime(1801, 11, 22),
                DateOfDeath = new DateTime(1872, 10, 4)
            };
            Author a4 = new Author
            {
                FirstName = "Чарльз",
                SecondName = "Паланик",
                Patronymic = "Майкл",
                DateOfBirth = new DateTime(1962, 02, 21)
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

            PublishingHouse ph2 = new PublishingHouse
            {
                Name = "Минкуль СССР",
                Country = "СССР",
                City = "Москва",
                FoundationDate = new DateTime(1953, 01, 01)
            };

            PublishingHouse ph3 = new PublishingHouse
            {
                Name = "Минздрав СССР",
                Country = "СССР",
                City = "Москва",
                FoundationDate = new DateTime(1933, 01, 01)
            };


            Book b1 = new Book
            {
                Capation = "Капитанская дочь",
                ISBN = "978-5-386-06612-3",
                AdditionalData = "классика",
            };
            Book b2 = new Book
            {
                Capation = "Кобзарь",
                ISBN = "978-5-770-72152-2",
                AdditionalData = "стишочки",
            };
            Book b3 = new Book
            {
                Capation = "Бойцовский клуб",
                ISBN = "978-5-170-81137-3",
                AdditionalData = "шизологический триллер-сатира",
            };

            context.Books.Add(b1);
            AuthorInBook ainb1 = new AuthorInBook
            {
                Author = a1,
                Book = b1
            };
            AuthorInBook ainb2 = new AuthorInBook
            {
                Author = a4,
                Book = b3
            };
            AuthorInBook ainb3 = new AuthorInBook
            {
                Author = a2,
                Book = b2
            };

            PublishedBook pb1 = new PublishedBook()
            {
                Book = b1,
                DateOfPublication = new DateTime(2013, 01, 01),
                PublishingHouse = ph1,
                Volume = 336
            };

            PublishedBook pb2 = new PublishedBook()
            {
                Book = b3,
                DateOfPublication = new DateTime(1996, 08, 17),
                PublishingHouse = ph1,
                Volume = 208
            };

            PublishedBook pb3 = new PublishedBook()
            {
                Book = b2,
                DateOfPublication = new DateTime(1840, 01, 01),
                PublishingHouse = ph1,
                Volume = 80
            };

            Periodical p1 = new Periodical
            {
                Name = "Мурзилка",
                Foundation = new DateTime(1924, 06, 16),
                IsPublished = true,
                ISSN = "0132-1943",
                Type = PeriodicalType.magazine,
                PublishingHouse = ph2
            };

            Periodical p2 = new Periodical
            {
                Name = "Фитиль",
                Foundation = new DateTime(1962, 07, 4),
                IsPublished = false,
                ISSN = "7777-8888",
                Type = PeriodicalType.newsreel,
                PublishingHouse = ph2
            };

            PeriodicalEdition pe1 = new PeriodicalEdition
            {
                Periodical = p1,
                Circulation = 45000,
                ReleaseNumber = 1,
                ReleaseDate = new DateTime(1924, 06, 16)
            };
            PeriodicalEdition pe2 = new PeriodicalEdition
            {
                Periodical = p1,
                Circulation = 45000,
                ReleaseNumber = 2,
                ReleaseDate = new DateTime(1962, 08, 13),
            };

            PeriodicalEdition pe3 = new PeriodicalEdition
            {
                Periodical = p2,
                Circulation = 1000,
                ReleaseNumber = 1,
                ReleaseDate = new DateTime(1962, 06, 16),
            };
            PeriodicalEdition pe4 = new PeriodicalEdition
            {
                Periodical = p2,
                Circulation = 1000,
                ReleaseNumber = 2,
                ReleaseDate = new DateTime(1962, 08, 13),
            };

            Brochure br1 = new Brochure
            {
                Capation = "О вреде курения",
                Circulation = 20000,
                PublishingHouse = ph3,
                ReleaseDate = new DateTime(1978, 02, 21),
                Volume = 20
            };

            Brochure br2 = new Brochure
            {
                Capation = "О вреде алкоголя",
                Circulation = 25000,
                PublishingHouse = ph3,
                ReleaseDate = new DateTime(1978, 02, 23),
                Volume = 20
            };

            ApplicationUserRole ur1 = new ApplicationUserRole
            {
                Name = "admin",
            };

            ApplicationUserRole ur2 = new ApplicationUserRole
            {
                Name = "user",
            };
            

            context.Roles.Add(ur1);
            context.Roles.Add(ur2);

            context.Authors.Add(a1);
            context.Authors.Add(a2);
            context.Authors.Add(a3);
            context.Authors.Add(a4);
            context.PublishingHouses.Add(ph1);
            context.PublishingHouses.Add(ph2);
            context.PublishingHouses.Add(ph3);
            context.AuthorsInBooks.Add(ainb1);
            context.AuthorsInBooks.Add(ainb2);
            context.AuthorsInBooks.Add(ainb3);
            context.PublishedBooks.Add(pb1);
            context.PublishedBooks.Add(pb2);
            context.PublishedBooks.Add(pb3);


            context.Periodicals.Add(p1);
            context.Periodicals.Add(p2);


            context.PeriodicalEditions.Add(pe1);
            context.PeriodicalEditions.Add(pe2);
            context.PeriodicalEditions.Add(pe3);
            context.PeriodicalEditions.Add(pe4);

            context.Brochures.Add(br1);
            context.Brochures.Add(br2);


            context.SaveChanges();


            base.Seed(context);
            var s = context.Authors.ToList();
        }


    }
}
