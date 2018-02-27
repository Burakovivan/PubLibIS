﻿using System.Data.Entity;

namespace PubLibIS_DAL.Model
{
    public class LibraryContext : DbContext
    {
        private static LibraryContext instance;
        private LibraryContext() : base("LibConnection")
        {
            Database.SetInitializer(new LibraryInitializer());
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Periodical> Periodicals { get; set; }
        public DbSet<PeriodicalEdition> PeriodicalEditions { get; set; }
        public DbSet<PublishingHouse> PublishingHouses { get; set; }

        public static LibraryContext GetInstance()
        {
            if (instance == null)
            {
                instance = new LibraryContext();
            }
            return instance;
        }
    }
}