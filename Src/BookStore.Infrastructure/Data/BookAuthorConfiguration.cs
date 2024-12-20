using BookStore.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Data;

internal class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
{
    public void Configure(EntityTypeBuilder<BookAuthor> builder)
    {
            builder.HasOne(e => e.Book)
            .WithMany()
            .HasForeignKey(e => e.BookId);
            builder.HasOne(e => e.Author)
            .WithMany()
            .HasForeignKey(e => e.AuthorId);
    }
}
