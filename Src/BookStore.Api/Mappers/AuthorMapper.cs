using Model = BookStore.Domain.Model;
using Dto = BookStore.Api.Dtos;
using CommonCore.Services;
using System.Linq.Expressions;

namespace BookStore.Api.Mappers;

public class AuthorMapper : MapperBase<Model.Author, Dto.Author>
{
    public override Dto.Author ToDto(Model.Author mod)
    {
        return new Dto.Author(mod.FullName, mod.DateOfBirth); 
    }
}
