using Microsoft.EntityFrameworkCore;
using PAWeb.Domain.Entities;
using PAWebApp.Domain.Entities;

namespace PAWeb.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "DbContext");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.TransactionArticles);

            modelBuilder.Entity<Transaction>()
                .HasMany(e => e.TransactionArticles);

            modelBuilder.Entity<Payment>()
                .HasOne(c => c.Customer);

            modelBuilder.Entity<TransactionArticle>()
            .HasKey(ta => new { ta.TransactionId, ta.ArticleId });

            modelBuilder.Entity<TransactionArticle>()
                .HasOne(ta => ta.Transaction)
                .WithMany(t => t.TransactionArticles)
                .HasForeignKey(ta => ta.TransactionId);
             
            modelBuilder.Entity<TransactionArticle>()
                .HasOne(ta => ta.Article)
                .WithMany(a => a.TransactionArticles)
                .HasForeignKey(ta => ta.ArticleId);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionArticle> TransactionArticles { get; set; }  

        //can be used so that 'InitialCreate' migration also adds some test data
        private static Customer[] DefaultCustomers()
        {
            return new Customer[]
            {
                new Customer { Id = 1, Name = "Adrian", Address = "Cluj Napoca" },
                new Customer { Id = 2, Name = "Karina", Address = "Oradea" },
                new Customer { Id = 3, Name = "Matei", Address = "Bucuresti" }
            };
        }

        private static Article[] DefaultArticle()
        {
            return new Article[]
            {
                new Article { Id = 1, Name = "Cola", Price = 10, Quantity  = 1000},
                new Article { Id = 2, Name = "Fanta", Price = 10, Quantity  = 2000},
                new Article { Id = 3, Name = "Sprite", Price = 10, Quantity  = 2000 }
            };
        }
    }
}
