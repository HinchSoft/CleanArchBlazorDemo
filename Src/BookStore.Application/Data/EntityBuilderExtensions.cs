using BookStore.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Data;

public static class EntityBuilderExtensions
{
    public static ComplexPropertyBuilder<Release> Configure(this ComplexPropertyBuilder<Release> builder)
    {
        builder.IsRequired();
        builder.Ignore(r=>r.Publisher);
        builder.Ignore(r => r.Edition);
        builder.Ignore(r=>r.Publication);
        builder.Property<Type>("PublicationKind")
            .UsePropertyAccessMode(Microsoft.EntityFrameworkCore.PropertyAccessMode.Property)
            .HasConversion(type => type.Name, name => PublicationKindToType(name));

        return builder;
    }

    private static Type PublicationKindToType(string kind) =>
        new Type[]
        {
            typeof(Published),
            typeof(Planned),
            typeof(NotPlanned)
        }.First(type=>type.Name == kind);
}
