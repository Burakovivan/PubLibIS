using System.Data.Entity;
using PubLibIS.DAL.Models;

namespace PubLibIS.DAL
{
    public class LibraryEntityFrameworkContext : DbContext
    {
        public LibraryEntityFrameworkContext(string connectionName = "ww") : base("LibConnection")
        {
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<System.DateTime>().Configure(c => c.HasColumnType("datetime2"));

            modelBuilder.Entity<PublishedBook>()
                .HasOptional(pb => pb.Book)
                .WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<PublishedBook>()
                .HasOptional(pb => pb.PublishingHouse)
                .WithMany()
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<Periodical>()
               .HasOptional(p => p.PublishingHouse)
               .WithMany()
               .WillCascadeOnDelete(true);

            modelBuilder.Entity<Brochure>()
               .HasOptional(p => p.PublishingHouse)
               .WithMany()
               .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

      
    }
}
