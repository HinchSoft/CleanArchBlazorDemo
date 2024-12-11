using System.Collections;

namespace BookStore.Domain.Model;

public class BookAuthorsCollectionWrapper : IAuthorsCollection
{
    private readonly List<Author> _authors = new ();

    public int Count => _authors.Count;

    public bool IsReadOnly => false;

    public void Add(Author item)
    {
        _authors.Add(item);
    }

    public void AppendMany(IEnumerable<Author> authors)
    {
        _authors.AddRange(authors);
        ReIndex();
    }

    public void Clear()
    {
        _authors.Clear();
    }

    public bool Contains(Author item)
    {
        return _authors.Contains(item);
    }

    public void CopyTo(Author[] array, int arrayIndex)
    {
        _authors.CopyTo(array,arrayIndex);
    }

    public IEnumerator<Author> GetEnumerator()
    {
        return _authors.GetEnumerator();
    }

    public int InsertBefore(Author author, Author target=null)
    {
        int idx = 0;
        if (_authors.Contains(author))
            _authors.Remove(author);
        if (target == null)
        {
            _authors.Insert(0, author);
        }
        else
        {
            idx = _authors.IndexOf(target);
            idx = (idx == -1) ? 0 : idx;
            _authors.Insert(idx, author);
        }
        ReIndex();
        return idx;
    }

    public bool Remove(Author item)
    {
        var ret = _authors.Remove(item);
        if (ret) ReIndex();
        return ret;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _authors.GetEnumerator();
    }

    private void ReIndex()
    {
        int i = 0;
        _authors.ForEach(a => a.Order = i++);
    }
}