using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PubLibIS.DAL.Enums;
using PubLibIS.DAL.Identity;
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
        private LibraryEntityFrameworkContext context;

        protected override void Seed(LibraryEntityFrameworkContext context)
        {
            this.context = context;

            ConfigurateCascade();
            SetTriggers();
            InitAuthor();
            InitPublishingHouse();
            InitBook();
            InitAuthorInBook();
            InitPublishedBook();
            InitPeriodical();
            InitPeriodicalEdition();
            InitBrochure();
            InitRolesAndUsers();

            var temp1 = context.Authors.Local.ToList();
            var temp2 = this.context.Authors.Local.ToList();

            this.context.SaveChanges();
            var temp = this.context.Authors.ToList();
            base.Seed(context);
        }

        private void ConfigurateCascade()
        {
            context.Database.ExecuteSqlCommand("ALTER TABLE Brochures DROP CONSTRAINT [FK_dbo.Brochures_dbo.PublishingHouses_PublishingHouse_Id]");
            context.Database.ExecuteSqlCommand("ALTER TABLE Brochures ADD CONSTRAINT [FK_dbo.Brochures_dbo.PublishingHouses_PublishingHouse_Id] FOREIGN KEY ([PublishingHouse_Id]) REFERENCES [dbo].[PublishingHouses] ([Id]) ON DELETE SET NULL");

            context.Database.ExecuteSqlCommand("ALTER TABLE Periodicals DROP CONSTRAINT [FK_dbo.Periodicals_dbo.PublishingHouses_PublishingHouse_Id]");
            context.Database.ExecuteSqlCommand("ALTER TABLE Periodicals ADD CONSTRAINT [FK_dbo.Periodicals_dbo.PublishingHouses_PublishingHouse_Id] FOREIGN KEY ([PublishingHouse_Id]) REFERENCES [dbo].[PublishingHouses] ([Id]) ON DELETE SET NULL");

            context.Database.ExecuteSqlCommand("ALTER TABLE PublishedBooks DROP CONSTRAINT [FK_dbo.PublishedBooks_dbo.PublishingHouses_PublishingHouse_Id]");
            context.Database.ExecuteSqlCommand("ALTER TABLE PublishedBooks ADD CONSTRAINT [FK_dbo.PublishedBooks_dbo.PublishingHouses_PublishingHouse_Id] FOREIGN KEY ([PublishingHouse_Id]) REFERENCES [dbo].[PublishingHouses] ([Id]) ON DELETE SET NULL");

        }

        private void SetTriggers()
        {

            string[] tableList = new[] { "Authors", "AuthorInBooks", "Books", "Brochures", "Periodicals", "PeriodicalEditions", "PublishedBooks", "PublishingHouses" };

            foreach(var table in tableList)
            {
                var query1 = 
$@"CREATE TRIGGER trigger_insert_{table}
        ON  [{table}]
        AFTER INSERT
    AS 
    BEGIN
        BEGIN
            UPDATE  [{table}]
            SET     [CreatedDate] = GETDATE(),
                    [ModifiedDate] =  GETDATE()
        END
    END";

                var query2 =
$@"CREATE TRIGGER trigger_update_{table}
        ON  [{table}]
        AFTER UPDATE
    AS 
    BEGIN
        BEGIN
            UPDATE  [{table}]
            SET     [ModifiedDate] = GETDATE()
        END
    END";

                context.Database.ExecuteSqlCommand(query1);
                context.Database.ExecuteSqlCommand(query2);
            }
        }

        private void InitAuthor()
        {
            var a1 = new Author
            {
                FirstName = "Александр",
                SecondName = "Пушкин",
                Patronymic = "Сергреевич",
                DateOfBirth = new DateTime(1799, 05, 26),
                DateOfDeath = new DateTime(1837, 01, 29)
            };
            var a2 = new Author
            {
                FirstName = "Тарас",
                SecondName = "Шевченко",
                Patronymic = "Григорьевич",
                DateOfBirth = new DateTime(1814, 03, 9),
                DateOfDeath = new DateTime(1861, 03, 10)
            };
            var a3 = new Author
            {
                FirstName = "Иван",
                SecondName = "Даль",
                Patronymic = "Владимирович",
                DateOfBirth = new DateTime(1801, 11, 22),
                DateOfDeath = new DateTime(1872, 10, 4)
            };

            var a4 = new Author
            {
                FirstName = "Чарльз",
                SecondName = "Паланик",
                Patronymic = "Майкл",
                DateOfBirth = new DateTime(1962, 02, 21)
            };
            context.Authors.Add(a1);
            context.Authors.Add(a2);
            context.Authors.Add(a3);
            context.Authors.Add(a4);
        }

        private void InitPublishingHouse()
        {
            var ph1 = new PublishingHouse
            {
                Name = "Весна",
                Country = "Украина",
                City = "Харьков",
                Address = "пр. Гагарина, д.20",
                PostalCode = "61010",
                Phone = "+38 (057) 760 23 89",
                FoundationDate = new DateTime(2016, 01, 01)
            };

            var ph2 = new PublishingHouse
            {
                Name = "Минкуль СССР",
                Country = "СССР",
                City = "Москва",
                FoundationDate = new DateTime(1953, 01, 01)
            };

            var ph3 = new PublishingHouse
            {
                Name = "Минздрав СССР",
                Country = "СССР",
                City = "Москва",
                FoundationDate = new DateTime(1933, 01, 01)
            };
            context.PublishingHouses.Add(ph1);
            context.PublishingHouses.Add(ph2);
            context.PublishingHouses.Add(ph3);
        }

        private void InitBook()
        {

            var b1 = new Book
            {
                Capation = "Капитанская дочь",
                ISBN = "978-5-386-06612-3",
                AdditionalData = "классика",
            };
            var b2 = new Book
            {
                Capation = "Кобзарь",
                ISBN = "978-5-770-72152-2",
                AdditionalData = "стишочки",
            };
            var b3 = new Book
            {
                Capation = "Бойцовский клуб",
                ISBN = "978-5-170-81137-3",
                AdditionalData = "шизологический триллер-сатира",
            };
            context.Books.Add(b1);
            context.Books.Add(b2);
            context.Books.Add(b3);
        }

        private void InitAuthorInBook()
        {
            var ainb1 = new AuthorInBook
            {
                Author = context.Authors.Local.ElementAtOrDefault(0),
                Book = context.Books.Local.ElementAtOrDefault(0)
            };
            var ainb2 = new AuthorInBook
            {
                Author = context.Authors.Local.ElementAtOrDefault(3),
                Book = context.Books.Local.ElementAtOrDefault(2)
            };

            var ainb3 = new AuthorInBook
            {
                Author = context.Authors.Local.ElementAtOrDefault(1),
                Book = context.Books.Local.ElementAtOrDefault(1)
            };

            context.AuthorsInBooks.Add(ainb1);
            context.AuthorsInBooks.Add(ainb2);
            context.AuthorsInBooks.Add(ainb3);
        }

        private void InitPublishedBook()
        {
            var pb1 = new PublishedBook()
            {
                Book = context.Books.Local.ElementAtOrDefault(0),
                DateOfPublication = new DateTime(2013, 01, 01),
                PublishingHouse = context.PublishingHouses.Local.ElementAtOrDefault(0),
                Volume = 336
            };

            var pb2 = new PublishedBook()
            {
                Book = context.Books.Local.ElementAtOrDefault(2),
                DateOfPublication = new DateTime(1996, 08, 17),
                PublishingHouse = context.PublishingHouses.Local.ElementAtOrDefault(0),
                Volume = 208
            };

            var pb3 = new PublishedBook()
            {
                Book = context.Books.Local.ElementAtOrDefault(1),
                DateOfPublication = new DateTime(1840, 01, 01),
                PublishingHouse = context.PublishingHouses.Local.ElementAtOrDefault(0),
                Volume = 80
            };
            context.PublishedBooks.Add(pb1);
            context.PublishedBooks.Add(pb2);
            context.PublishedBooks.Add(pb3);
        }

        private void InitPeriodical()
        {
            var p1 = new Periodical
            {
                Name = "Мурзилка",
                Foundation = new DateTime(1924, 06, 16),
                IsPublished = true,
                ISSN = "0132-1943",
                Type = PeriodicalType.magazine,
                PublishingHouse = context.PublishingHouses.Local.ElementAtOrDefault(0)
            };

            var p2 = new Periodical
            {
                Name = "Фитиль",
                Foundation = new DateTime(1962, 07, 4),
                IsPublished = false,
                ISSN = "7777-8888",
                Type = PeriodicalType.newsreel,
                PublishingHouse = context.PublishingHouses.Local.ElementAtOrDefault(1)
            };
            context.Periodicals.Add(p1);
            context.Periodicals.Add(p2);
        }

        private void InitPeriodicalEdition()
        {
            var pe1 = new PeriodicalEdition
            {
                Periodical = context.Periodicals.Local.ElementAtOrDefault(0),
                Circulation = 45000,
                ReleaseNumber = 1,
                ReleaseDate = new DateTime(1924, 06, 16)
            };
            var pe2 = new PeriodicalEdition
            {
                Periodical = context.Periodicals.Local.ElementAtOrDefault(0),
                Circulation = 45000,
                ReleaseNumber = 2,
                ReleaseDate = new DateTime(1962, 08, 13),
            };
            var pe3 = new PeriodicalEdition
            {
                Periodical = context.Periodicals.Local.ElementAtOrDefault(1),
                Circulation = 1000,
                ReleaseNumber = 1,
                ReleaseDate = new DateTime(1962, 06, 16),
            };
            var pe4 = new PeriodicalEdition
            {
                Periodical = context.Periodicals.Local.ElementAtOrDefault(1),
                Circulation = 1000,
                ReleaseNumber = 2,
                ReleaseDate = new DateTime(1962, 08, 13),
            };

            context.PeriodicalEditions.Add(pe1);
            context.PeriodicalEditions.Add(pe2);
            context.PeriodicalEditions.Add(pe3);
            context.PeriodicalEditions.Add(pe4);
        }

        private void InitBrochure()
        {
            var br1 = new Brochure
            {
                Capation = "О вреде курения",
                Circulation = 20000,
                PublishingHouse = context.PublishingHouses.Local.ElementAtOrDefault(2),
                ReleaseDate = new DateTime(1978, 02, 21),
                Volume = 20
            };

            var br2 = new Brochure
            {
                Capation = "О вреде алкоголя",
                Circulation = 25000,
                PublishingHouse = context.PublishingHouses.Find(2),
                ReleaseDate = new DateTime(1978, 02, 23),
                Volume = 20
            };
            context.Brochures.Add(br1);
            context.Brochures.Add(br2);
        }

        private void InitRolesAndUsers()
        {

            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationUserRole>(context));
            var r1 = new ApplicationUserRole
            {
                Name = "admin"
            };
            var r2 = new ApplicationUserRole
            {
                Name = "user"
            };
            roleManager.CreateAsync(r1).GetAwaiter().GetResult();
            roleManager.CreateAsync(r2).GetAwaiter().GetResult();

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var u1 = new ApplicationUser
            {
                UserName = "admin@lh.ua",
                Email = "admin@lh.ua",
            };
            var u2 = new ApplicationUser
            {
                UserName = "user@lh.ua",
                Email = "user@lh.ua",
            };
            userManager.CreateAsync(u1, "password").GetAwaiter().GetResult();
            userManager.CreateAsync(u2, "password").GetAwaiter().GetResult();
            var userId1 = userManager.FindByEmailAsync("admin@lh.ua").GetAwaiter().GetResult().Id;
            var userId2 = userManager.FindByEmailAsync("user@lh.ua").GetAwaiter().GetResult().Id;
            userManager.AddToRoleAsync(userId1, "admin").GetAwaiter().GetResult();
            userManager.AddToRoleAsync(userId2, "user").GetAwaiter().GetResult();
        }
    }
}
