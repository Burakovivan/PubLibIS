using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using PubLibIS.DAL.Models;

namespace PubLibIS.DAL
{
    public class LibraryEntityFrameworkContext : IdentityDbContext<ApplicationUser>
    {
        public LibraryEntityFrameworkContext(string connectionName) : base(connectionName)
        {
            if (connectionName == "LibConnection")
                Database.SetInitializer(new LibraryInitializer());
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorInBook> AuthorsInBooks { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Brochure> Brochures { get; set; }
        public DbSet<Periodical> Periodicals { get; set; }
        public DbSet<PeriodicalEdition> PeriodicalEditions { get; set; }
        public DbSet<PublishingHouse> PublishingHouses { get; set; }
        public DbSet<PublishedBook> PublishedBooks { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<System.DateTime>().Configure(c => c.HasColumnType("datetime2"));

            modelBuilder.Entity<PublishedBook>()
                .HasRequired(pb => pb.Book)
                .WithMany(pb => pb.PublishedBooks);

            modelBuilder.Entity<PublishedBook>()
                .HasOptional(p => p.PublishingHouse)
                .WithMany(p => p.Books)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Periodical>()
               .HasOptional(p => p.PublishingHouse)
               .WithMany(p => p.Periodicals)
               .HasForeignKey(p => p.PublishingHouse_Id);

            modelBuilder.Entity<PeriodicalEdition>()
               .HasRequired(p => p.Periodical)
               .WithMany(p => p.PeriodicalEditions);

            modelBuilder.Entity<Brochure>()
               .HasOptional(p => p.PublishingHouse)
               .WithMany(b => b.Brochures)
               .HasForeignKey(b => b.PublishingHouse_Id);

            

            base.OnModelCreating(modelBuilder);
        }



    }
}
