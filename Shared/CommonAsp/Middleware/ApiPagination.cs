using CommonCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonAsp.Middleware;

public class ApiPagination
{
    private readonly RequestDelegate _next;

    public ApiPagination(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var pi = new PaginationInfo();

        var page = context.Request.Query["page"];
        var perPage = context.Request.Query["per_page"];
        if (int.TryParse(page, out int iPage))
            pi.Page = iPage;
        if (int.TryParse(perPage, out int iPPage))
            pi.PerPage = iPPage;

        context.Items.Add(Constants.PageInfoItemName, pi);

        context.Response.OnStarting(() =>
        {
            if(pi.PageCount>0)
            {
                var hdrs = new LinkHeaderBuilder(context.Request,pi.PerPage);

                hdrs.Add(1, LinkHeaderBuilder.Rel.First);
                if(pi.Page>1)hdrs.Add(pi.Page-1, LinkHeaderBuilder.Rel.Previous);
                if(pi.Page<pi.PageCount)hdrs.Add(pi.Page+1, LinkHeaderBuilder.Rel.Next);
                hdrs.Add(pi.PageCount, LinkHeaderBuilder.Rel.Last);
                if(!context.Response.HasStarted)
                {
                    context.Response.Headers.Add(hdrs.ToHeader());
                }
               
            }

            return Task.CompletedTask;
        });

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}
