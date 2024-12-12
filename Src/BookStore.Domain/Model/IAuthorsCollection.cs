
namespace BookStore.Domain.Model
{
    public interface IAuthorsCollection:ICollection<Author>
    {
        void AppendMany(IEnumerable<Author> authors);
        int InsertAfter(Author author, Author? target = null);
        int InsertBefore(Author author, Author? target = null);
    }
}