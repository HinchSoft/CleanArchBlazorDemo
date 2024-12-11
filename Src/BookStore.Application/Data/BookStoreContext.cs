using BookStore.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(b =>
            {
                b.Ignore(e => e.Order);
            });

            modelBuilder.Entity<BookAuthor>(b =>
            {
                b.HasOne(e => e.Book)
                .WithMany()
                .HasForeignKey(e => e.BookId);
                b.HasOne(e => e.Author)
                .WithMany()
                .HasForeignKey(e => e.AuthorId);
            });

            modelBuilder.Entity<Book>().Configure();

        }
    }
}
