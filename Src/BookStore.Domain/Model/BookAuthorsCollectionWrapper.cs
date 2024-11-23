namespace BookStore.Domain.Model;

public class BookAuthorsCollectionWrapper : IAuthorsCollection
{
    public BookAuthorsCollectionWrapper(Book book, ICollection<Author> authors)
    {

    }

    public void AppendMany(IEnumerable<Author> authors)
    {
        throw new NotImplementedException();
    }
}