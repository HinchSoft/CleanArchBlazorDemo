using BookStore.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Data;

public static class EntityBuilderExtensions
{
    public static EntityTypeBuilder<Book> Configure(this EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Ignore(e => e.Publisher);
        builder.Ignore(e => e.Release);

        builder.HasMany(e => e.Authors).WithMany()
            .UsingEntity(typeof(BookAuthor));

        builder.Property(e => e.Culture)
            .HasConversion(culture => culture.Name, name => new System.Globalization.CultureInfo(name));

        // register Release as a complex type
        //builder.ComplexProperty(e => e.Release).Configure();
        //builder.HasOne(b => b.Publisher)
        //    .WithMany().HasForeignKey("PublisherId");

        return builder;
    }

    public static ComplexPropertyBuilder<Release> Configure(this ComplexPropertyBuilder<Release> builder)
    {
        builder.IsRequired();
        builder.Ignore(r => r.Publisher);

        builder.Property(r => r.Edition)
            .HasMaxLength(11)
            .HasConversion(e => EncodeEdition(e), c => DecodeEdition(c));

        builder.Property<Type>("PublicationKind")
            .HasConversion(type => type.Name, name => PublicationKindToType(name));
        builder.Property<PublicationDate>("PublicationDate")
            .HasConversion(pub => pub.ToString(), date => PublicationDate.Parse(date));
        return builder;
    }

    private static string EncodeEdition(IEdition edition)
    {
        return edition switch
        {
            Ordinal ordinal => $"{ordinal.Number}",
            Seasonal seasonal => $"{Enum.GetName(seasonal.Season)} {seasonal.Year}"
        };
    }

    private static IEdition DecodeEdition(string code)
    {
        return new Ordinal(1);
    }

    private static Type PublicationKindToType(string kind) =>
        new Type[]
        {
            typeof(Published),
            typeof(Planned),
            typeof(NotPlannedYet)
        }.First(type => type.Name == kind);


}
