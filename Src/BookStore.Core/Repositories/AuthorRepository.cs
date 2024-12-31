using BookStore.Domain.Model;
using CommonCore.Repositories;
using CommonCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Repositories;

public sealed class AuthorRepository
{
    private readonly IRepository<Author> _repository;
    private readonly PaginationService<Author> _paginationService;
    private readonly MapService<Author> _mapService;


    public AuthorRepository(IRepository<Author> repository,
        PaginationService<Author> paginationService,
        MapService<Author> mapService)
    {
        _repository = repository;
        _paginationService = paginationService;
        _mapService = mapService;
    }

    public IAsyncEnumerable<T> GetAllAsync<T>()
    {
        var mapper = _mapService.GetMapper<T>();

        var query = _repository.GetQueryable();
        query=_paginationService.Filtering(query,mapper);
        query=_paginationService.Ordering(query, mapper);
        query=_paginationService.Paginate(query);

        return _repository.AsAsyncEnumerable(query.Select(m=>mapper.ToDto(m)));
    }

}
