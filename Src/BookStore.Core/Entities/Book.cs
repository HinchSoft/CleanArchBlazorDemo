using CommonCore.Repositories;
using System.Globalization;

namespace BookStore.Domain.Model
{
    public class Book:Entity
    {
        public string Title { get; private set; }
        public CultureInfo Culture { get; private set; }
        public IEdition Edition { get; private set; }

        public Release Release { get;  private set; }

        public Publisher Publisher {  get; private set; }

        public IAuthorsCollection Authors { get; set; } = new BookAuthorsCollectionWrapper();

        public Book (string title, CultureInfo culture, IEdition edition, Release release,Publisher publisher, ICollection<Author> authors)
        {
            Title = title;
            Culture = culture;
            Release = release;
            Authors.AppendMany(authors);
            Publisher = publisher;
            Edition = edition;
        }   

        private Book()  // Used by EF Core
        {
        }

    }
}
