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

        var AuthorFaker = new Faker<Author>()
            .RuleFor(c => c.FullName, f => f.Name.FullName())
            .RuleFor(c=> c.DateOfBirth, f=>f.Date.Past(100,DateTime.UtcNow.AddYears(-16)));

        var publisherFaker = new Faker<Publisher>()
            .RuleFor(p => p.Name, f => f.Name.FullName());

        var publishers = publisherFaker.Generate(50);

        var authors = AuthorFaker.Generate(50);

        await context.Publishers.AddRangeAsync(publishers);
        await context.Authors.AddRangeAsync(authors);

        var bookFaker = new Faker<Book>()
            .CustomInstantiator(f =>
            {
                var title = f.Lorem.Slug(Randomizer.Seed.Next(1, 4));
                var culture = f.PickRandom(sampleCultures);
                var bookauths = f.PickRandom(authors, f.Random.Int(1, 3)).ToList();
                var bookPub = f.PickRandom(publishers);
                var pubInfo = new NotPlannedYet();
                var release = new Release(bookPub, new Ordinal(f.Random.Int(1, 10)),pubInfo);
                return new Book(title, culture, release, bookauths);
            });

        var books = bookFaker.Generate(100);


        await context.Books.AddRangeAsync(books);

        return await context.SaveChangesAsync();
    }
}
