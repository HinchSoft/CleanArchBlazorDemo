using System.Runtime.CompilerServices;
using Model = BookStore.Domain.Model;

namespace BookStore.Api.Dtos;

public static class MappingExtensions
{

    public static Book MapToDto(this Model.Book mod)
    {
        return new Book(mod.Title, mod.Culture.ToString(),
            mod.Edition?.ToString(),
            mod.Release.MapToDto());
    }

    public static Release MapToDto(this Model.Release mod)
    {
        return new Release(mod.Publication.ToString());
    }
}
