using CommonAsp;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCore.Services;

public class PageInfoProvider : IPageInfoProvider
{
    public PageInfoProvider(IHttpContextAccessor httpContextAccessor)
    {
        var pi = httpContextAccessor.HttpContext?.Items[Constants.PageInfoItemName] as PaginationInfo
            ?? new PaginationInfo();
        
        PaginationInfo = pi;
    }

    public PaginationInfo PaginationInfo { get; private set; }
}
