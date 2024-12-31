using CommonCore.Services;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

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
        var filter = context.Request.Query["filter"];
        var order = context.Request.Query["orderby"];

        if (int.TryParse(page, out int iPage))
            pi.Page = iPage;
        if (int.TryParse(perPage, out int iPPage))
            pi.PerPage = iPPage;

        pi.Filter = GetFeildList(filter, @"^(?<Field>\w+)(?<Operator>\W+)(?<Value>.*)");
        pi.OrderBy = GetFeildList(order, @"^(?<Field>\S*)\((?<Operator>.*)\)","Asc");

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

    private static PaginationInfo.FieldList[] GetFeildList(string? query,string pattern,string? DefVal = null,char seperator=',')
    {
        var ret=new List<PaginationInfo.FieldList>();

        if (!string.IsNullOrEmpty(query))
        {
            var regex = new Regex(pattern);
            var flds = query.Split(seperator);
            foreach (var fld in flds)
            {
                string? fldname, fldoperator;
                string? fldvalue=null;

                var match=regex.Match(fld);
                if (match != null && match.Success)
                {
                    fldname = match.Groups.GetValueOrDefault("Field")?.Value;
                    fldoperator = match.Groups.GetValueOrDefault("Operator")?.Value;
                    fldvalue = match.Groups.GetValueOrDefault("Value")?.Value;
                }
                else if (!string.IsNullOrEmpty(DefVal))
                {
                    fldname = fld;
                    fldoperator = DefVal;
                }
                else continue;

                if(fldname!=null && fldoperator!=null)
                    ret.Add(new PaginationInfo.FieldList(fldname, fldoperator, fldvalue));
            }
        }
        return [.. ret];
    }
}
