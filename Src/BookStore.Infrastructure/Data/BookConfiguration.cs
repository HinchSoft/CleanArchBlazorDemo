
using BookStore.Domain.Model;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Data;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey("Id");

        builder.Ignore(e => e.Release);

        builder.HasMany(e => e.Authors).WithMany()
            .UsingEntity(typeof(BookAuthor));

        builder.Property(e => e.Culture)
            .HasConversion(culture => culture.Name, name => new System.Globalization.CultureInfo(name));

        builder.ComplexProperty(e => e.Release, builder => {
            builder.Ignore(e => e.Publication);
            builder.Property<Type>("PublicationKind")
                .HasConversion(type => type.Name, name => PublicationKindToType(name));
            builder.Property<PublicationDate>("PublicationDate")
                .HasConversion(pub => pub.ToString(), date => PublicationDate.Parse(date)); 
        });
        builder.HasOne(e => e.Publisher)
            .WithMany()
            .HasForeignKey("PublisherID");
        builder.Property(e => e.Edition)
            .HasConversion(e => EncodeEdition(e), s => DecodeEdition(s));
    }

    private static Type PublicationKindToType(string kind) =>
    new Type[]
    {
            typeof(Published),
            typeof(Planned),
            typeof(NotPlannedYet)
    }.First(type => type.Name == kind);

    private static string EncodeEdition(IEdition edition)
    {
        return edition.ToString();
    }

    private static IEdition DecodeEdition(string code)
    {
        if(int.TryParse(code,out int ord))
        {
            return new Ordinal(ord);
        }
        var v = code.Split(' ');
        var s = Enum.Parse<Season>(v[0]);
        var y = int.Parse(v[1]);
        return new Seasonal(s,y);
    }

}
