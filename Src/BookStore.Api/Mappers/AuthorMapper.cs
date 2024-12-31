using Model = BookStore.Domain.Model;
using Dto = BookStore.Api.Dtos;
using CommonCore.Services;
using System.Linq.Expressions;
using System.Collections;

namespace BookStore.Api.Mappers;

public class AuthorMapper : MapperBase<Model.Author, Dto.Author>
{
    Hashtable AuthorMap = new Hashtable
    {
        { nameof(Dto.Author.FirstName), nameof(Model.Author.FirstName) },
        { nameof(Dto.Author.LastName) , nameof(Model.Author.LastName)},
    };
    public override string NameFromDto(string name)
    {
        foreach (DictionaryEntry item in AuthorMap)
            if (((string)item.Value).Equals(name, StringComparison.InvariantCultureIgnoreCase))
                return (string)item.Value;
        return string.Empty;
    }

    public override Dto.Author ToDto(Model.Author mod)
    {
        return new Dto.Author(mod.FirstName, mod.LastName, mod.DateOfBirth); 
    }
}
