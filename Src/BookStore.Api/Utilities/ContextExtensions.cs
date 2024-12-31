using Bogus;
using BookStore.Domain.Model;
using System.Globalization;
using BookStore.Infrastructure.Data;

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

        var pubDates = new Faker<PublicationDate>[]
        {
            new Faker<PublicationDate>()
                .CustomInstantiator(f =>
                {
                    return new FullDate(f.Date.PastDateOnly(10));
                }),
            new Faker<PublicationDate>()
                .CustomInstantiator(f =>
                {
                    var dt=f.Date.PastDateOnly(10);
                    return new YearMonth(dt.Year,dt.Month);
                }),
            new Faker<PublicationDate>()
                .CustomInstantiator(f =>
                {
                    var dt=f.Date.PastDateOnly(10);
                    return new YearOnly(dt.Year);
                }),
        };

        var pubInfos = new Faker<PublicationInfo>[]
        {
            new Faker<PublicationInfo>()
                .CustomInstantiator(f =>
                {
                    return new NotPlannedYet();
                }),
            new Faker<PublicationInfo>()
                .CustomInstantiator(f =>
                {
                    var date=f.PickRandom(pubDates).Generate();
                    return new Planned(date);
                }),
            new Faker<PublicationInfo>()
                .CustomInstantiator(f =>
                {
                    var date=f.PickRandom(pubDates).Generate();
                    return new Published(date);
                })
        };

        var editions = new Faker<IEdition>[]
        {
            new Faker<IEdition>()
                .CustomInstantiator(f =>
                {
                    return new Ordinal(f.Random.Int(1, 10));
                }),
            new Faker<IEdition>()
                .CustomInstantiator(f =>
                {
                    return new Seasonal(f.PickRandom<Season>(),f.Date.PastDateOnly(10).Year);
                })
        };

        var AuthorFaker = new Faker<Author>()
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.DateOfBirth, f => f.Date.Past(100, DateTime.UtcNow.AddYears(-16)));

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
                var pubInfo = f.PickRandom(pubInfos).Generate();
                var bookEdi = f.PickRandom(editions).Generate();

                var release = new Release(pubInfo);
                return new Book(title, culture, bookEdi, release, bookPub, bookauths);
            });

        var books = bookFaker.Generate(100);


        await context.Books.AddRangeAsync(books);

        return await context.SaveChangesAsync();
    }
}
