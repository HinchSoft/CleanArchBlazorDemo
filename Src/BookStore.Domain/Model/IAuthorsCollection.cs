
namespace BookStore.Domain.Model
{
    public interface IAuthorsCollection
    {
        void AppendMany(IEnumerable<Author> authors);
    }
}