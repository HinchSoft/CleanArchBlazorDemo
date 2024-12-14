using System.Runtime.CompilerServices;
using Model = BookStore.Domain.Model;

namespace BookStore.Api.Dtos;

public static class MappingExtensions
{
    public static Author MapToDto(this Model.Author mod)
    {
        return new Author(mod.FullName,mod.DateOfBirth);
    }

    public static Book MapToDto(this Model.Book mod)
    {
        return new Book(mod.Title, mod.Culture.ToString(),
            mod.Edition?.ToString(),
            mod.Authors.Select(a => a.MapToDto()).ToArray(),
            mod.Publisher?.Name,
            mod.Release.MapToDto());
    }

    public static Release MapToDto(this Model.Release mod)
    {
        return new Release(mod.Publication.ToString());
    }
}
