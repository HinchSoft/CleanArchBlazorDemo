using CommonCore.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace CommonCore.Services;

public class PaginationService<TEntity> where TEntity:Entity 
{
    private readonly PaginationInfo _paginationInfo;
    private readonly IOptions<PaginationOptions> _options;
    private readonly IRepository<TEntity> _repository;
    public PaginationService(IOptions<PaginationOptions> options, IRepository<TEntity> repository,
        IPageInfoProvider? pageInfoProvider = null)
    {
        _paginationInfo = pageInfoProvider?.PaginationInfo ?? new PaginationInfo();
        _options = options;
        _repository = repository;
    }

    public IQueryable<TEntity> Filtering(IQueryable<TEntity> query,IMapper mapper)
    {
        if(_paginationInfo.Filter.Count()==0)
            return query;
        Type entityType = typeof(TEntity);
        foreach (var field in _paginationInfo.Filter)
        {

            var fieldName = mapper.NameFromDto(field.Field);
            if (entityType.GetProperty(fieldName) == null)
                continue;


            query = _repository.ApplyFilteringByName(fieldName, field.Operator, field.Value , query);
        }


        return query;
    }

    public IQueryable<TEntity> Ordering(IQueryable<TEntity> query, IMapper mapper)
    {
        if(_paginationInfo.OrderBy.Count()==0)
            return query;
        Type entityType = typeof(TEntity);

        foreach (var field in _paginationInfo.OrderBy)
        {
            var fieldName = mapper.NameFromDto(field.Field);
            if (entityType.GetProperty(fieldName) == null)
                continue;

            bool asc = field.Operator.ToLower() != "desc";

            query = _repository.ApplyOrderingByName(fieldName, asc, query);
        }

        return query;
    }

    public IQueryable<T> Paginate<T>(IQueryable<T> query)
        where T : Entity
    {
        var defPageSize = _options.Value.DefaultPageSize;

        var perPage = _paginationInfo.PerPage;
        var pageNo = _paginationInfo.Page;
        if (defPageSize > 0 && perPage==0)
        {
            perPage=defPageSize;
            pageNo = 1;
        }

        if (pageNo > 0 && perPage > 0)
        {
            if (query is not IOrderedQueryable<T>)
                query = query.OrderBy(e => e.Id);

            var tot = query.Count();

            _paginationInfo.PageCount = (int)Math.Ceiling((double)tot / perPage);

            var take = perPage;
            var skip = (pageNo - 1) * take;

            query = query.Skip(skip).Take(take);
        }

        return query;

    }

}
