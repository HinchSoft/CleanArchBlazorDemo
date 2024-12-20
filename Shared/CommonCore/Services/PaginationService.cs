using CommonCore.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCore.Services;

public class PaginationService
{
    private readonly PaginationInfo _paginationInfo;
    public PaginationService(IPageInfoProvider? pageInfoProvider = null)
    {
        _paginationInfo = pageInfoProvider?.PaginationInfo ?? new PaginationInfo();
    }

    public IQueryable<T> Paginate<T>(IQueryable<T> query)
        where T : Entity
    {
        if (_paginationInfo.Page > 0 && _paginationInfo.PerPage > 0)
        {
            if (query is not IOrderedQueryable<T>)
                query = query.OrderBy(e => e.Id);

            var tot = query.Count();

            _paginationInfo.PageCount = (int)Math.Ceiling((double)tot / _paginationInfo.PerPage);

            // Skip and take to be after any ordering
            var take = _paginationInfo.PerPage;
            var skip = (_paginationInfo.Page - 1) * take;

            query = query.Skip(skip).Take(take);
        }

        return query;

    }
}
