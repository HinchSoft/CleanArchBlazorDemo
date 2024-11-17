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

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Culture)
                    .HasConversion(culture => culture.Name, name => new System.Globalization.CultureInfo(name));
                // register Release as a complex type
                b.ComplexProperty(e => e.Release).Configure();
            });
        }
    }
}
