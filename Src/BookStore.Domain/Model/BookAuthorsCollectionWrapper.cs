using System.ComponentModel.Design;

namespace BookStore.Domain.Model;

public class BookAuthorsCollectionWrapper : IAuthorsCollection
{
    private readonly List<Author> _authors = new();
    public BookAuthorsCollectionWrapper(Book book, ICollection<Author> authors)
    {
        _authors.AddRange(authors);
    }

    public void AppendMany(IEnumerable<Author> authors)
    {
        _authors.AddRange(authors);
    }

    public int InsertBefore(Author author, Author target=null)
    {
        if(_authors.Contains(author))
            _authors.Remove(author);
        if (target == null)
        {
            _authors.Insert(0, author);
            return 0;
        }
        else
        {
            var idx = _authors.IndexOf(target);
            idx = (idx == -1) ? 0 : idx;
            _authors.Insert(idx, author);
            return idx;
        }
    }
}