using Bogus;
using BookStore.Domain.Model;
using System.Globalization;
using BookStore.Application.Data;

namespace BookStore.Api.Utilities;

public static class ContextExtensions
{
    public static async Task<int> SeedDb(this BookStoreContext context)
    {
        Randomizer.Seed = new Random(159357);

        var sampleCultures = new CultureInfo[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("en-GB"),
            new CultureInfo("fr-FR")
        };

        var bookFaker = new Faker<Book>()
            .RuleFor(c => c.Title, f => f.Lorem.Slug(Randomizer.Seed.Next(1, 4)))
            .RuleFor(c => c.Culture, f => f.PickRandom(sampleCultures) );

        var AuthorFaker = new Faker<Author>()
            .RuleFor(c => c.FullName, f => f.Name.FullName());

        var authors = AuthorFaker.Generate(50);

        var publisherFaker = new Faker<Publisher>()
            .RuleFor(p => p.Name, f => f.Name.FullName());

        var publishers = publisherFaker.Generate(50);

        var books = bookFaker.Generate(100);
        foreach ( var book in books )
        {

        }


        await context.Books.AddRangeAsync(books);

        return await context.SaveChangesAsync();
    }
}
