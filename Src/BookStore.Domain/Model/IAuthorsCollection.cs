
namespace BookStore.Domain.Model
{
    public interface IAuthorsCollection:ICollection<Author>
    {
        void AppendMany(IEnumerable<Author> authors);
    }
}